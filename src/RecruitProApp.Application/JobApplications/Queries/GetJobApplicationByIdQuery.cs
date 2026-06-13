using MediatR;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.JobApplications.Queries
{
    public record GetJobApplicationByIdQuery(Guid Id) : IRequest<JobApplication?>;

}
