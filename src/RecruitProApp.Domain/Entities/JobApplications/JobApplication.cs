using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications.Enums;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Domain.Entities.JobApplications
{
    public class JobApplication
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public int Score { get; private set; } = 0;
        public JobApplicationStatus Status { get; private set; } = JobApplicationStatus.PENDING;
        public DateTime AppliedAt { get; private set; }

        public Guid OfferId { get; private set; }
        public virtual Offer? Offer { get; set; }

        public Guid CandidateId { get; private set; }
        public virtual Candidate? Candidate { get; set; }

        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();

        
        public JobApplication(Guid offerId, Guid candidateId, DateTime appliedAt, JobApplicationStatus status)
        {
            OfferId = offerId;
            CandidateId = candidateId;
            AppliedAt = appliedAt;
            Status = status;
        }

        public void UpdateStatus(JobApplicationStatus status)
        {
            Status = status;
        }
        
        public void UpdateScore(int score)
        {
            Score = score;
        }
    }
}
