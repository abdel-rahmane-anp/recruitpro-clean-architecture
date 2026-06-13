namespace RecruitProApp.Domain.Entities.Interviews
{
    public interface IInterviewRepository
    {
        Task<IEnumerable<Interview>> GetAllAsync(CancellationToken cancellationToken);
        Task<Interview?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Interview interview, CancellationToken cancellationToken);
        Task UpdateAsync(Interview interview, CancellationToken cancellationToken);
        Task DeleteAsync(Guid interviewId, CancellationToken cancellationToken);
    }
}
