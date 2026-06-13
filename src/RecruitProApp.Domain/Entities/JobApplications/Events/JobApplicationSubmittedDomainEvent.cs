using RecruitProApp.Domain.Common;

namespace RecruitProApp.Domain.Entities.JobApplications.Events
{
    public sealed record JobApplicationSubmittedDomainEvent(Guid JobApplicationId, Guid CandidateId) : IDomainEvent;
}
