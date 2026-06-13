using MediatR;
using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Application.JobApplications.Commands.RejectApplication
{
    public class RejectJobApplicationHandler : IRequestHandler<RejectJobApplicationCommand, Unit>
    {
        private readonly IJobApplicationWorkflowService _workflow;

        public RejectJobApplicationHandler(IJobApplicationWorkflowService workflow)
        {
            _workflow = workflow;
        }

        public async Task<Unit> Handle(RejectJobApplicationCommand request, CancellationToken cancellationToken)
        {
            await _workflow.RejectApplicationAsync(request.JobApplicationId, request.Reason);
            return Unit.Value;
        }
    }
}
