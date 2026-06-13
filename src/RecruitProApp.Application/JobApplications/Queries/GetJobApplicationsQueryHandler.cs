using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Application.JobApplications.Queries
{
    public class GetJobApplicationsQueryHandler : IRequestHandler<GetJobApplicationsQuery, IEnumerable<JobApplication>>
    {
        private readonly IJobApplicationRepository _repo;

        public GetJobApplicationsQueryHandler(IJobApplicationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<JobApplication>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }

}
