using MediatR;
using RecruitProApp.Application.Interviews.DTOs;

namespace RecruitProApp.Application.Interviews.Queries.Get
{
    public record GetInterviewsByApplicationIdQuery(Guid JobApplicationId) : IRequest<List<InterviewDto>>;    
}
