using MediatR;

namespace RecruitProApp.Application.JobApplications.Commands.AcceptJobApplication
{
    public record AcceptJobApplicationCommand(Guid JobApplicationId) : IRequest<Unit>;
}
