using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.JobApplications.Enums;

namespace RecruitProApp.Application.JobApplications.Commands.CreateJobApplication
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Guid>
    {
        private readonly IJobApplicationRepository _jobApplicationreporepo;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IJobApplicationWorkflowService _workflow;

        public CreateJobApplicationHandler(IJobApplicationRepository jobApplicationreporepo,
            ICandidateRepository candidateRepository, IOfferRepository offerRepository, IJobApplicationWorkflowService jobApplicationWorkflowService)
        {
            _jobApplicationreporepo = jobApplicationreporepo;
            _candidateRepository = candidateRepository;
            _offerRepository = offerRepository;
            _workflow = jobApplicationWorkflowService;
        }

        public async Task<Guid> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestData(request, cancellationToken);

            var application = new JobApplication(
                request.OfferId,
                request.CandidateId,
                    DateTime.UtcNow,
                JobApplicationStatus.PENDING
            );

            await _jobApplicationreporepo.AddAsync(application, cancellationToken);
            await _workflow.ProcessNewApplicationAsync(application);

            return application.Id;
        }

        private async Task ValidateRequestData(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            if (!await _offerRepository.ExistsAsync(request.OfferId, cancellationToken))
                throw new Exception("Aucune offre correspondante à cet ID");

            if (!await _candidateRepository.ExistsAsync(request.CandidateId, cancellationToken))
                throw new Exception("Aucun candidat correspondant à cet ID");
        }
    }

}
