using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RecruitProApp.Infrastructure.Persistence
{
    public class RecruitProAppDbContextFactory : IDesignTimeDbContextFactory<RecruitProAppDbContext>
    {
        public RecruitProAppDbContext CreateDbContext(string[] args)
        {
            // Root path of the startup project RecruitProApp.WebAPI (where appsettings.json lives).
            var routePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../RecruitProApp.WebAPI"));

            var config = new ConfigurationBuilder()
                .SetBasePath(routePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("DefaultConnection is missing in appsettings.json");

            var optionBuilder = new DbContextOptionsBuilder<RecruitProAppDbContext>();
            optionBuilder.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure());

            return new RecruitProAppDbContext(optionBuilder.Options, new NoOpPublisher());
        }
    }
}
