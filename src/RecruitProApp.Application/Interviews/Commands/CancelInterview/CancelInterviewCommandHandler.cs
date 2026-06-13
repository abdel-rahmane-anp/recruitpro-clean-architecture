using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Interviews;

namespace RecruitProApp.Application.Interviews.Commands.CancelInterview
{
    public class CancelInterviewCommandHandler : IRequestHandler<CancelInterviewCommand, Unit>
    {
        private readonly IInterviewRepository _interviewRepository;

        public CancelInterviewCommandHandler(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public async Task<Unit> Handle(CancelInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = await _interviewRepository.GetByIdAsync(request.InterviewId, cancellationToken);
            if (interview == null) throw new KeyNotFoundException("Entretien introuvable.");

            await _interviewRepository.DeleteAsync(interview.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
