using Microsoft.EntityFrameworkCore;
using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Application.Common.Interfaces
{
    public interface IRecruitProAppDbContext
    {
        DbSet<Offer> Offers { get; }
        DbSet<JobApplication> JobApplications { get; }
        DbSet<Candidate> Candidates { get; }
        DbSet<Interview> Interviews { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
