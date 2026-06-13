using MediatR;

namespace RecruitProApp.Application.JobApplications.Commands.CreateJobApplication
{
    public record CreateJobApplicationCommand(Guid CandidateId, Guid OfferId) : IRequest<Guid>;
}
