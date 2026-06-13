using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications.Events;

namespace RecruitProApp.Application.JobApplications.Events
{
    /// <summary>Sends a confirmation email when an application is submitted.</summary>
    public sealed class JobApplicationSubmittedDomainEventHandler
        : INotificationHandler<JobApplicationSubmittedDomainEvent>
    {
        private readonly ICandidateRepository _candidates;
        private readonly IEmailCustomService _email;

        public JobApplicationSubmittedDomainEventHandler(ICandidateRepository candidates, IEmailCustomService email)
        {
            _candidates = candidates;
            _email = email;
        }

        public async Task Handle(JobApplicationSubmittedDomainEvent notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidates.GetByIdAsync(notification.CandidateId, cancellationToken);
            if (candidate is null) return;

            await _email.SendAsync(candidate.Email.Value, "Application received",
                "Thank you, your application has been received.");
        }
    }
}
