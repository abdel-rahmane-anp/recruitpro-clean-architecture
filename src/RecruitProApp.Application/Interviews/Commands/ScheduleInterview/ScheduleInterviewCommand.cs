using MediatR;

namespace RecruitProApp.Application.Interviews.Commands.ScheduleInterview
{
    public record ScheduleInterviewCommand(
        Guid JobApplicationId,
        DateTime ScheduledAt,
        string? Link,
        string? Notes
    ) : IRequest<Guid>;
}
