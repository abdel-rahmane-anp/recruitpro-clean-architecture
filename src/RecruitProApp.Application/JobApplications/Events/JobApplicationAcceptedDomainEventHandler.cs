using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications.Events;

namespace RecruitProApp.Application.JobApplications.Events
{
    /// <summary>Notifies the candidate when their application is accepted.</summary>
    public sealed class JobApplicationAcceptedDomainEventHandler
        : INotificationHandler<JobApplicationAcceptedDomainEvent>
    {
        private readonly ICandidateRepository _candidates;
        private readonly IEmailCustomService _email;

        public JobApplicationAcceptedDomainEventHandler(ICandidateRepository candidates, IEmailCustomService email)
        {
            _candidates = candidates;
            _email = email;
        }

        public async Task Handle(JobApplicationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidates.GetByIdAsync(notification.CandidateId, cancellationToken);
            if (candidate is null) return;

            await _email.SendAsync(candidate.Email.Value, "Congratulations",
                "Your application has been accepted.");
        }
    }
}
