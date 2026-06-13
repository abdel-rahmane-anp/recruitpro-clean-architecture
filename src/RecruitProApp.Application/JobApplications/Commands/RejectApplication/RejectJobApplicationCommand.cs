using MediatR;

namespace RecruitProApp.Application.JobApplications.Commands.RejectApplication
{
    public record RejectJobApplicationCommand(Guid JobApplicationId, string Reason) : IRequest<Unit>;
}
