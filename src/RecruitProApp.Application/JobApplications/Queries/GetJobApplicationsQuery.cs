using MediatR;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.JobApplications.Queries
{
    public record GetJobApplicationsQuery : IRequest<IEnumerable<JobApplication>>;

}
