using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Candidates.Commands
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Guid>
    {
        private readonly ICandidateRepository _repository;

        public CreateCandidateCommandHandler(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = Candidate.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.ResumeUrl);

            await _repository.AddAsync(candidate, cancellationToken);
            return candidate.Id;
        }
    }
}
