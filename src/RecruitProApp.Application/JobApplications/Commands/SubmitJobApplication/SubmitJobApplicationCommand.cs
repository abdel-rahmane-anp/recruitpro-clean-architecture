using MediatR;

namespace RecruitProApp.Application.JobApplications.Commands.SubmitJobApplication
{
    public record SubmitJobApplicationCommand(
        Guid CandidateId, 
        Guid OfferId
    ) : IRequest<Guid>;
}
