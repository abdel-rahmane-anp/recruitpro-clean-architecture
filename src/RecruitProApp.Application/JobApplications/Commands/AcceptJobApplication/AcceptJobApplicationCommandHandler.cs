using MediatR;
using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Application.JobApplications.Commands.AcceptJobApplication
{
    public class AcceptJobApplicationHandler : IRequestHandler<AcceptJobApplicationCommand, Unit>
    {
        private readonly IJobApplicationWorkflowService _workflow;

        public AcceptJobApplicationHandler(IJobApplicationWorkflowService workflow)
        {
            _workflow = workflow;
        }

        public async Task<Unit> Handle(AcceptJobApplicationCommand request, CancellationToken cancellationToken)
        {
            await _workflow.AcceptApplicationAsync(request.JobApplicationId);
            return Unit.Value;
        }
    }
}
