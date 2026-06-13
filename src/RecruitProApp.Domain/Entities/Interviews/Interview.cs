using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Domain.Entities.Interviews
{
    public class Interview
    {
        public Guid Id { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        public string Link { get; private set; } = string.Empty;
        public string Notes { get; private set; } = string.Empty;

        public Guid JobApplicationId { get; private set; }
        public virtual JobApplication JobApplication { get; set; } = default!;
        
        public Interview(DateTime scheduledAt, string link, string notes, Guid jobApplicationId)
        {
            ScheduledAt = scheduledAt;
            Link = link;
            Notes = notes;
            JobApplicationId = jobApplicationId;
        }

        public void UpdateLink(string link)
        {
            Link = link;
        }
        
        public void UpdateScheduledDate(DateTime date)
        {
            ScheduledAt = date;
        }
    }

}
