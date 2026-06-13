using Microsoft.EntityFrameworkCore;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Infrastructure.Persistence;

namespace RecruitProApp.Infrastructure.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly RecruitProAppDbContext _context;

        public InterviewRepository(RecruitProAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Interview interview, CancellationToken cancellationToken)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Interviews.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Interview>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Interviews
                .ToListAsync(cancellationToken);
        }

        public async Task<Interview?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Interviews
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Interview interview, CancellationToken cancellationToken)
        {
            _context.Interviews.Update(interview);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid interviewId, CancellationToken cancellationToken)
        {
            var interview = await this.GetByIdAsync(interviewId, cancellationToken);

            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();                
            }
        }
    }
}
