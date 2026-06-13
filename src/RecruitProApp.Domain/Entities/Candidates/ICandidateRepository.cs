using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Common.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync(CancellationToken cancellationToken);
        Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task  AddAsync(Candidate candidate, CancellationToken cancellationToken);
    }
}
