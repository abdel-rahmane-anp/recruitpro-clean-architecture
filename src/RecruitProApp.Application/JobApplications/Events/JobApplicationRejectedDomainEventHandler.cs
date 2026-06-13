using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications.Events;

namespace RecruitProApp.Application.JobApplications.Events
{
    /// <summary>Notifies the candidate when their application is rejected.</summary>
    public sealed class JobApplicationRejectedDomainEventHandler
        : INotificationHandler<JobApplicationRejectedDomainEvent>
    {
        private readonly ICandidateRepository _candidates;
        private readonly IEmailCustomService _email;

        public JobApplicationRejectedDomainEventHandler(ICandidateRepository candidates, IEmailCustomService email)
        {
            _candidates = candidates;
            _email = email;
        }

        public async Task Handle(JobApplicationRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var candidate = await _candidates.GetByIdAsync(notification.CandidateId, cancellationToken);
            if (candidate is null) return;

            await _email.SendAsync(candidate.Email, "Application update",
                $"Your application was not retained. Reason: {notification.Reason}");
        }
    }
}
