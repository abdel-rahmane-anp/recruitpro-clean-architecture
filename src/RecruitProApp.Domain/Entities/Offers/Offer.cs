using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Domain.Entities.Offers
{
    public class Offer
    {
        public Offer(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime PublicationDate { get; private set; }

        public ICollection<JobApplication> JobApplications { get; set; } = [];
    }
}
