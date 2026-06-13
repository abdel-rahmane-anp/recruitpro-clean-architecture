using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.Interviews.DTOs;
using RecruitProApp.Domain.Entities.Interviews;

namespace RecruitProApp.Application.Interviews.Queries.Get
{
    public class GetInterviewsByApplicationIdQueryHandler : IRequestHandler<GetInterviewsByApplicationIdQuery, List<InterviewDto>>
    {
        private readonly IInterviewRepository _interviewRepository;

        public GetInterviewsByApplicationIdQueryHandler(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public async Task<List<InterviewDto>> Handle(GetInterviewsByApplicationIdQuery request, CancellationToken cancellationToken)
        {
            var interviews = await _interviewRepository.GetAllAsync(cancellationToken);
            
            return interviews
                .Select(i => new InterviewDto(
                    i.Id,
                    i.ScheduledAt,
                    i.Link,
                    i.Notes
                ))
                .ToList();
        }
    }
}
