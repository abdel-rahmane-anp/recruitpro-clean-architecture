using RecruitProApp.Domain.Entities.JobApplications.Enums;

namespace RecruitProApp.Application.JobApplications.DTOs
{
    public record JobApplicationDto(
       Guid Id,
       Guid OfferId,
       Guid CandidateId,
       DateTime AppliedAt,
       JobApplicationStatus Status
    );
}
