using RecruitProApp.Domain.Common;
using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications.Enums;
using RecruitProApp.Domain.Entities.JobApplications.Events;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Domain.Entities.JobApplications
{
    /// <summary>
    /// Aggregate root representing a candidate's application to an offer.
    /// Every state change goes through an intent-revealing method that enforces
    /// the business rules and raises the relevant domain event.
    /// </summary>
    public class JobApplication : AggregateRoot
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int Score { get; private set; }
        public JobApplicationStatus Status { get; private set; } = JobApplicationStatus.PENDING;
        public DateTime AppliedAt { get; private set; }

        public Guid OfferId { get; private set; }
        public virtual Offer? Offer { get; private set; }

        public Guid CandidateId { get; private set; }
        public virtual Candidate? Candidate { get; private set; }

        public virtual ICollection<Interview> Interviews { get; private set; } = new List<Interview>();

        // Required by EF Core.
        private JobApplication() { }

        private JobApplication(Guid offerId, Guid candidateId)
        {
            Id = Guid.NewGuid();
            OfferId = offerId;
            CandidateId = candidateId;
            AppliedAt = DateTime.UtcNow;
            Status = JobApplicationStatus.PENDING;
        }

        /// <summary>Factory: a candidate submits an application for an offer.</summary>
        public static JobApplication Submit(Guid offerId, Guid candidateId)
        {
            if (offerId == Guid.Empty)
                throw new DomainException("An application must reference a valid offer.");
            if (candidateId == Guid.Empty)
                throw new DomainException("An application must reference a valid candidate.");

            var application = new JobApplication(offerId, candidateId);
            application.RaiseDomainEvent(new JobApplicationSubmittedDomainEvent(application.Id, candidateId));
            return application;
        }

        /// <summary>
        /// Assigns a score (0-100). A strong score (&gt;= 70) automatically
        /// preselects an application that is still pending.
        /// </summary>
        public void SetScore(int score)
        {
            if (score is < 0 or > 100)
                throw new DomainException("Score must be between 0 and 100.");

            Score = score;

            if (Status == JobApplicationStatus.PENDING && score >= 70)
                Status = JobApplicationStatus.PRESELECTED;
        }

        public void Accept()
        {
            EnsureNotClosed();
            Status = JobApplicationStatus.ACCEPTED;
            RaiseDomainEvent(new JobApplicationAcceptedDomainEvent(Id, CandidateId));
        }

        public void Reject(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("A rejection requires a reason.");

            EnsureNotClosed();
            Status = JobApplicationStatus.REJECTED;
            RaiseDomainEvent(new JobApplicationRejectedDomainEvent(Id, CandidateId, reason));
        }

        private void EnsureNotClosed()
        {
            if (Status is JobApplicationStatus.ACCEPTED or JobApplicationStatus.REJECTED)
                throw new DomainException($"A {Status} application can no longer change state.");
        }
    }
}
