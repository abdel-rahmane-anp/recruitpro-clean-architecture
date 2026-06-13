using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.JobApplications.Enums;

namespace RecruitProApp.Application.JobApplications.Commands.SubmitJobApplication
{
    public class SubmitJobApplicationCommandHandler : IRequestHandler<SubmitJobApplicationCommand, Guid>
    {
        private readonly IJobApplicationRepository _repository;

        public SubmitJobApplicationCommandHandler(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(SubmitJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = new JobApplication(
                request.OfferId,
                request.CandidateId,
                    DateTime.UtcNow,
                JobApplicationStatus.PENDING
            );

            await _repository.AddAsync(application, cancellationToken);
            return application.Id;
        }
    }
}
