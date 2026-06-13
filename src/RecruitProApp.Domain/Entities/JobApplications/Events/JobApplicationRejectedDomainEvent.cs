using RecruitProApp.Domain.Common;

namespace RecruitProApp.Domain.Entities.JobApplications.Events
{
    public sealed record JobApplicationRejectedDomainEvent(Guid JobApplicationId, Guid CandidateId, string Reason) : IDomainEvent;
}
