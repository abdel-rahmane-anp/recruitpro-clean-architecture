using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications.Enums;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Infrastructure.Services
{
    public class JobApplicationWorkflowService : IJobApplicationWorkflowService
    {
        private readonly IJobApplicationRepository _repo;
        private readonly IEmailCustomService _emailService;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public JobApplicationWorkflowService(IJobApplicationRepository repo, IEmailCustomService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public async Task ProcessNewApplicationAsync(JobApplication app)
        {
            await ScoreApplicationAsync(app);
            await SendConfirmationEmailAsync(app);

            // Changement automatique de statut si score > 70%
            if (app.Score >= 70)
            {
                app.UpdateStatus(JobApplicationStatus.PRESELECTED);
                await _repo.UpdateAsync(app, _cancellationToken);
            }
        }

        public async Task ScoreApplicationAsync(JobApplication app)
        {
            // TODO: à remplacer par du NLP ou parsing de CV
            app.UpdateScore(new Random().Next(50, 100));
            await _repo.UpdateAsync(app, _cancellationToken);
        }

        public async Task SendConfirmationEmailAsync(JobApplication app)
        {
            await _emailService.SendAsync(app.Candidate!.Email, "Votre candidature a bien été reçue !", "Merci...");
        }

        public async Task AcceptApplicationAsync(Guid applicationId)
        {
            var app = await _repo.GetByIdAsync(applicationId, _cancellationToken);
            if (app == null)
                throw new Exception("Candidature non trouvée");

            app.UpdateStatus(JobApplicationStatus.ACCEPTED);

            await _repo.UpdateAsync(app, _cancellationToken);

            await _emailService.SendAsync(app.Candidate!.Email, "Félicitations", "Vous avez été retenu.");
        }

        public async Task RejectApplicationAsync(Guid applicationId, string reason)
        {
            var app = await _repo.GetByIdAsync(applicationId, _cancellationToken);
            if (app == null)
                throw new Exception("Candidature non trouvée");

            app.UpdateStatus(JobApplicationStatus.REJECTED);

            await _repo.UpdateAsync(app, _cancellationToken);

            await _emailService.SendAsync(app.Candidate!.Email, "Candidature rejetée", $"Motif : {reason}");
        }

        public async Task ScheduleInterviewAsync(Guid applicationId, DateTime interviewDate)
        {
            var app = await _repo.GetByIdAsync(applicationId, _cancellationToken);
            if (app == null)
                throw new Exception("Candidature non trouvée");

            app.UpdateStatus(JobApplicationStatus.INTERVIEW_SCHEDULED);

            await _repo.UpdateAsync(app, _cancellationToken);
            // Save `interviewDate` dans une future entité `Interview` ?
            
            await _emailService.SendAsync(app.Candidate!.Email, "Entretien programmé", $"Entretien prévu le {interviewDate}");
        }
    }

}
