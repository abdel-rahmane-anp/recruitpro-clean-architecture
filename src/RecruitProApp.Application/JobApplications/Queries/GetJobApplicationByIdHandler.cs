using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.JobApplications.Queries
{
    public class GetJobApplicationByIdHandler : IRequestHandler<GetJobApplicationByIdQuery, JobApplication?>
    {
        private readonly IJobApplicationRepository _repo;

        public GetJobApplicationByIdHandler(IJobApplicationRepository repo)
        {
            _repo = repo;
        }

        public async Task<JobApplication?> Handle(GetJobApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id, cancellationToken);
        }
    }

}
