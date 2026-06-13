using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Domain.Entities.Candidates
{
    public class Candidate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ResumeUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
