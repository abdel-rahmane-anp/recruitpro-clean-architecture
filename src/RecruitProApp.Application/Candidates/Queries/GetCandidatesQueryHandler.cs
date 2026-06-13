using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Candidates.Queries
{
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, IEnumerable<Candidate>>
    {
        private readonly ICandidateRepository _repository;

        public GetCandidatesQueryHandler(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Candidate>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
