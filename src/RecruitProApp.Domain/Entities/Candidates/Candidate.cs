using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.ValueObjects;

namespace RecruitProApp.Domain.Entities.Candidates
{
    public class Candidate
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Email Email { get; private set; } = default!;
        public string Phone { get; private set; } = string.Empty;
        public string ResumeUrl { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public virtual ICollection<JobApplication> JobApplications { get; private set; } = new List<JobApplication>();

        // Required by EF Core.
        private Candidate() { }

        /// <summary>Factory: registers a new candidate with a validated email.</summary>
        public static Candidate Register(string firstName, string lastName, string email, string phone, string resumeUrl)
        {
            return new Candidate
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = new Email(email),
                Phone = phone,
                ResumeUrl = resumeUrl,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
