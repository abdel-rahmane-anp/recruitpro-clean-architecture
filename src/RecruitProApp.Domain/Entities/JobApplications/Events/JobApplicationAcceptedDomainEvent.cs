using RecruitProApp.Domain.Common;

namespace RecruitProApp.Domain.Entities.JobApplications.Events
{
    public sealed record JobApplicationAcceptedDomainEvent(Guid JobApplicationId, Guid CandidateId) : IDomainEvent;
}
