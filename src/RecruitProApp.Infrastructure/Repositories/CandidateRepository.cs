using Microsoft.EntityFrameworkCore;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Infrastructure.Persistence;

namespace RecruitProApp.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly RecruitProAppDbContext _context;

        public CandidateRepository(RecruitProAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Candidate candidate, CancellationToken cancellationToken)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Candidates.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Include(x => x.JobApplications)
                .ToListAsync(cancellationToken);
        }

        public async Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Candidates.FindAsync(id, cancellationToken);
        }
    }
}
