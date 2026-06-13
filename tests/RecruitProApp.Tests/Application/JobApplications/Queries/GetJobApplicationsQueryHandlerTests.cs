using NSubstitute;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.JobApplications.Queries;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.JobApplications.Enums;

namespace RecruitProApp.Tests.Application.JobApplications.Queries
{
    public class GetJobApplicationsQueryHandlerTests
    {
        private readonly IJobApplicationRepository _repository;
        private readonly GetJobApplicationsQueryHandler _handler;

        public GetJobApplicationsQueryHandlerTests()
        {
            _repository = Substitute.For<IJobApplicationRepository>();
            _handler = new GetJobApplicationsQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_Should_Return_List_Of_JobApplications()
        {
            var fakeData = new List<JobApplication>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, JobApplicationStatus.PENDING)
            };

            _repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(fakeData);

            var result = await _handler.Handle(new GetJobApplicationsQuery(), CancellationToken.None);

            Assert.Single(result);
            Assert.Equal(JobApplicationStatus.PENDING, result.First().Status);
        }
    }
}
