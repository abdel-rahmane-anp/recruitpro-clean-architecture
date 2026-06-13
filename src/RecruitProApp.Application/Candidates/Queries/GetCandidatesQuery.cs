using MediatR;
using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Candidates.Queries
{
    public record GetCandidatesQuery() : IRequest<IEnumerable<Candidate>>;
}
