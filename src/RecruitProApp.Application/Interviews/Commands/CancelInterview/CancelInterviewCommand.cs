using MediatR;

namespace RecruitProApp.Application.Interviews.Commands.CancelInterview
{
    public record CancelInterviewCommand(Guid InterviewId) : IRequest<Unit>;
}
