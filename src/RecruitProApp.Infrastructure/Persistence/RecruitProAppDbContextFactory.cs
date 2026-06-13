using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace RecruitProApp.Infrastructure.Persistence
{
    public class RecruitProAppDbContextFactory : IDesignTimeDbContextFactory<RecruitProAppDbContext>
    {
        public RecruitProAppDbContext CreateDbContext(string[] args)
        {
            // Chemin racine du projet de démarrage RecruitProApp.WebAPI (là où se trouve appsettings.json)
            var routePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../RecruitProApp.WebAPI"));

            var config = new ConfigurationBuilder()
                .SetBasePath(routePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Develoment.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("DefaultConnection is missing in appsettings.json");

            var optionBuilder = new DbContextOptionsBuilder<RecruitProAppDbContext>();
            optionBuilder.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure());

            return new RecruitProAppDbContext(optionBuilder.Options);
        }
    }
}
