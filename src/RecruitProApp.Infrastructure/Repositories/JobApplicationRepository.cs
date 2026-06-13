using Microsoft.EntityFrameworkCore;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Infrastructure.Persistence;
using static System.Net.Mime.MediaTypeNames;

namespace RecruitProApp.Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly RecruitProAppDbContext _context;

        public JobApplicationRepository(RecruitProAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobApplication application, CancellationToken cancellationToken)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.JobApplications.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.JobApplications
                .ToListAsync(cancellationToken);
        }

        public async Task<JobApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.JobApplications
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(JobApplication jobApplication, CancellationToken cancellationToken)
        {
            _context.JobApplications.Update(jobApplication);
            await _context.SaveChangesAsync();
        }
    }
}
