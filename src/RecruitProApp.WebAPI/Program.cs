using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RecruitProApp.Application;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Infrastructure;
using RecruitProApp.Infrastructure.Persistence;
using RecruitProApp.Infrastructure.Repositories;
using Serilog;

// Conf Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Configure Cors
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

//Récupère la chaîne de connexion depuis appsettings.json
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultConnectionString))
{
    throw new InvalidOperationException("DefaultConnection string is not configured in the application settings.");
}

// Ajoute la couche Infrastructure (DbContext, Repositories...)
builder.Services.AddInfrastructure(defaultConnectionString);

// Ajoute la couche Application (CQRS, Handlers, Services...)
builder.Services.AddApplication();

//Ajoute Serilog : remplacer le logger par Serilog
builder.Host.UseSerilog();
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.AddAzureMonitorLogExporter(o =>
    {
        o.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
    });
});

// Add OpenTelemtry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("RecruitProApp"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddAzureMonitorTraceExporter(options =>
            {
                options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
            });
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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