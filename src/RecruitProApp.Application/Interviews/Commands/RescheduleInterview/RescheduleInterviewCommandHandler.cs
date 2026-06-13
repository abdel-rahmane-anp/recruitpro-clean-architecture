using MediatR;
using RecruitProApp.Domain.Entities.Interviews;

namespace RecruitProApp.Application.Interviews.Commands.RescheduleInterview
{
    public class RescheduleInterviewCommandHandler : IRequestHandler<RescheduleInterviewCommand, Unit>
    {
        private readonly IInterviewRepository _interviewRepository;

        public RescheduleInterviewCommandHandler(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public async Task<Unit> Handle(RescheduleInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = await _interviewRepository.GetByIdAsync(request.InterviewId, cancellationToken);
            if (interview == null) 
                throw new KeyNotFoundException("Entretien introuvable.");

            interview.UpdateScheduledDate(request.NewDate);
            interview.UpdateLink(request.NewLink ?? interview.Link);

            await _interviewRepository.UpdateAsync(interview, cancellationToken);

            return Unit.Value;
        }
    }
}
