using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities;
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
            var candidate = new Candidate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                ResumeUrl = request.ResumeUrl
            };

            await _repository.AddAsync(candidate, cancellationToken);
            return candidate.Id;
        }
    }
}
