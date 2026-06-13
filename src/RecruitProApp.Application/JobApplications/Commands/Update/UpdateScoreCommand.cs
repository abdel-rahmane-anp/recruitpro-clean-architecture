using MediatR;

namespace RecruitProApp.Application.JobApplications.Commands.Update
{
    public record UpdateScoreCommand(Guid ApplicationId, int Score) : IRequest<Unit>;
}
