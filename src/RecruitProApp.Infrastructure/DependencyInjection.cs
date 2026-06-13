using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecruitProApp.Application;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Infrastructure.Persistence;
using RecruitProApp.Infrastructure.Repositories;
using RecruitProApp.Infrastructure.Services;
using System.Reflection;

namespace RecruitProApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecruitProAppDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

            // Add DbContext
            services.AddScoped<IRecruitProAppDbContext, RecruitProAppDbContext>(provider => provider.GetRequiredService<RecruitProAppDbContext>());

            // Add repositories
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();

            // Add services
            services.AddScoped<IEmailCustomService, EmailCustomService>();
            services.AddScoped<IJobApplicationWorkflowService, JobApplicationWorkflowService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Ajoute MediatR (Query/Command Handlers)
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());

            // enregistrer ici d'autres services spécifiques à la couche Application


            return services;
        }
    }
}
