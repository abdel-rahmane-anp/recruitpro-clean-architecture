using MediatR;
using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Application.JobApplications.Commands.RejectApplication
{
    public class RejectJobApplicationHandler : IRequestHandler<RejectJobApplicationCommand, Unit>
    {
        private readonly IJobApplicationRepository _repository;

        public RejectJobApplicationHandler(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RejectJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.GetByIdAsync(request.JobApplicationId, cancellationToken)
                ?? throw new KeyNotFoundException("Job application not found.");

            application.Reject(request.Reason);

            await _repository.UpdateAsync(application, cancellationToken);

            return Unit.Value;
        }
    }
}
