using MediatR;
using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Application.JobApplications.Commands.AcceptJobApplication
{
    public class AcceptJobApplicationHandler : IRequestHandler<AcceptJobApplicationCommand, Unit>
    {
        private readonly IJobApplicationRepository _repository;

        public AcceptJobApplicationHandler(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AcceptJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.GetByIdAsync(request.JobApplicationId, cancellationToken)
                ?? throw new KeyNotFoundException("Job application not found.");

            application.Accept();

            await _repository.UpdateAsync(application, cancellationToken);

            return Unit.Value;
        }
    }
}
