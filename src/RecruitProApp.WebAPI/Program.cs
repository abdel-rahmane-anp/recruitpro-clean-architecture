using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RecruitProApp.Application;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Infrastructure;
using RecruitProApp.Infrastructure.Persistence;
using RecruitProApp.Infrastructure.Repositories;
using RecruitProApp.WebAPI.Middleware;
using Serilog;

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

// Read the connection string from configuration (appsettings or the
// ConnectionStrings__DefaultConnection environment variable).
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultConnectionString))
{
    throw new InvalidOperationException("DefaultConnection string is not configured in the application settings.");
}

// Infrastructure layer (DbContext, repositories...)
builder.Services.AddInfrastructure(defaultConnectionString);

// Application layer (CQRS handlers, MediatR...)
builder.Services.AddApplication();

// Replace the default logger with Serilog
builder.Host.UseSerilog();

// Application Insights / Azure Monitor is OPTIONAL.
// It is only wired up when a connection string is provided, e.g. via the
// ApplicationInsights__ConnectionString environment variable. This keeps the
// app fully runnable locally and in containers without any Azure dependency.
var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
var azureMonitorEnabled = !string.IsNullOrWhiteSpace(appInsightsConnectionString);

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    if (azureMonitorEnabled)
    {
        options.AddAzureMonitorLogExporter(o => o.ConnectionString = appInsightsConnectionString);
    }
});

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("RecruitProApp"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        if (azureMonitorEnabled)
        {
            tracerProviderBuilder.AddAzureMonitorTraceExporter(o => o.ConnectionString = appInsightsConnectionString);
        }
    });

// Global exception handling -> RFC 7807 ProblemDetails responses
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply pending EF Core migrations at startup so the application is runnable
// out of the box (e.g. via `docker compose up`). Transient connection errors
// while the database container is starting are retried thanks to
// EnableRetryOnFailure configured on the DbContext.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecruitProAppDbContext>();
    dbContext.Database.Migrate();
}

// Must be first in the pipeline so it wraps every downstream component.
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

app.Run();
