using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecruitProApp.Application;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Infrastructure.Persistence;
using RecruitProApp.Infrastructure.Repositories;
using RecruitProApp.Infrastructure.Services;

namespace RecruitProApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecruitProAppDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

            services.AddScoped<IRecruitProAppDbContext, RecruitProAppDbContext>(
                provider => provider.GetRequiredService<RecruitProAppDbContext>());

            // Repositories
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();

            // Services
            services.AddScoped<IEmailCustomService, EmailCustomService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());

            return services;
        }
    }
}
