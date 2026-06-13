using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications.Enums;

namespace RecruitProApp.Application.Interviews.Commands.ScheduleInterview
{
    public class ScheduleInterviewCommandHandler : IRequestHandler<ScheduleInterviewCommand, Guid>
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IEmailCustomService _emailCustomService;

        public ScheduleInterviewCommandHandler(IInterviewRepository interviewRepository, IJobApplicationRepository jobApplicationRepository,
            IEmailCustomService emailCustomService)
        {
            _interviewRepository = interviewRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _emailCustomService = emailCustomService;
        }

        public async Task<Guid> Handle(ScheduleInterviewCommand request, CancellationToken cancellationToken)
        {
            // Vérifier que la candidature existe
            var jobApp = await _jobApplicationRepository.GetByIdAsync(request.JobApplicationId, cancellationToken);
            if (jobApp == null)
                throw new KeyNotFoundException("Candidature introuvable.");

            // Ne pas planifier si refusée
            if (jobApp.Status == JobApplicationStatus.REJECTED)
                throw new InvalidOperationException("Impossible de planifier une interview pour une candidature rejetée.");

            var interview = new Interview(
                request.ScheduledAt,
                request.Link ?? string.Empty,
                request.Notes ?? string.Empty,
                request.JobApplicationId
            );

            await _interviewRepository.AddAsync(interview, cancellationToken);

            await _emailCustomService.SendAsync(
                "rh@entreprise.com", 
                "Entretien planifié", 
                $"Un entretien est prévu le {request.ScheduledAt}.\nLien: {request.Link}");

            return interview.Id;
        }
    }
}
