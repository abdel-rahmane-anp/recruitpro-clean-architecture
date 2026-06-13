using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Candidates.Queries
{
    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Candidate?>
    {
        private readonly ICandidateRepository _repository;

        public GetCandidateByIdQueryHandler(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<Candidate?> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
