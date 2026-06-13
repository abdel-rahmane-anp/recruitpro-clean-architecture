using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.JobApplications.Commands.CreateJobApplication
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Guid>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IOfferRepository _offerRepository;

        public CreateJobApplicationHandler(
            IJobApplicationRepository jobApplicationRepository,
            ICandidateRepository candidateRepository,
            IOfferRepository offerRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _candidateRepository = candidateRepository;
            _offerRepository = offerRepository;
        }

        public async Task<Guid> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestData(request, cancellationToken);

            var application = JobApplication.Submit(request.OfferId, request.CandidateId);

            await _jobApplicationRepository.AddAsync(application, cancellationToken);

            return application.Id;
        }

        private async Task ValidateRequestData(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            if (!await _offerRepository.ExistsAsync(request.OfferId, cancellationToken))
                throw new KeyNotFoundException("No offer matches this id.");

            if (!await _candidateRepository.ExistsAsync(request.CandidateId, cancellationToken))
                throw new KeyNotFoundException("No candidate matches this id.");
        }
    }
}
