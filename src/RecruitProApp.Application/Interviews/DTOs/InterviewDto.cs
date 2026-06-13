namespace RecruitProApp.Application.Interviews.DTOs
{
    public record InterviewDto
    (
        Guid Id,
        DateTime ScheduledAt,
        string? Link,
        string? Notes
    );
}
