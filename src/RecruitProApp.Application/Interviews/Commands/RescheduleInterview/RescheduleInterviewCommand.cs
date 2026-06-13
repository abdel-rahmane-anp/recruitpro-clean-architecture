using MediatR;

namespace RecruitProApp.Application.Interviews.Commands.RescheduleInterview
{
    public record RescheduleInterviewCommand(
        Guid InterviewId,
        DateTime NewDate,
        string? NewLink
    ) : IRequest<Unit>;
}
