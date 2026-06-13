using MediatR;
using RecruitProApp.Domain.Entities.Candidates;

namespace RecruitProApp.Application.Candidates.Queries
{
    public record GetCandidateByIdQuery(Guid Id) : IRequest<Candidate?>;
}
