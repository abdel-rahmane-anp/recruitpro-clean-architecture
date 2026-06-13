using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.Common.Interfaces
{
    public interface IJobApplicationWorkflowService
    {
        Task ProcessNewApplicationAsync(JobApplication app);
        Task ScoreApplicationAsync(JobApplication app);
        Task SendConfirmationEmailAsync(JobApplication app);
        Task ScheduleInterviewAsync(Guid applicationId, DateTime interviewDate);
        Task AcceptApplicationAsync(Guid applicationId);
        Task RejectApplicationAsync(Guid applicationId, string reason);
    }

}
