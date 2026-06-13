using MediatR;

namespace RecruitProApp.Application.Candidates.Commands
{
    public record CreateCandidateCommand(string FirstName, string LastName, string Email, string Phone, string ResumeUrl) 
        : IRequest<Guid>;
}
