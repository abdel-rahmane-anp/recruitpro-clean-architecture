using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.Common.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllAsync(CancellationToken cancellationToken);
        Task<JobApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task  AddAsync(JobApplication jobApplication, CancellationToken cancellationToken);
        Task  UpdateAsync(JobApplication jobApplication, CancellationToken cancellationToken);
    }
}
