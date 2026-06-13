using AutoFixture;
using NSubstitute;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.JobApplications.Commands.SubmitJobApplication;
using RecruitProApp.Domain.Entities.JobApplications;

namespace RecruitProApp.Tests.Application.JobApplications.Commands
{
    public class SubmitJobApplicationCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IJobApplicationRepository _repository;
        private readonly SubmitJobApplicationCommandHandler _handler;

        public SubmitJobApplicationCommandHandlerTests()
        {
            _fixture = new Fixture();
            _repository = Substitute.For<IJobApplicationRepository>();
            _handler = new SubmitJobApplicationCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_Should_Add_JobApplication_And_Return_Id()
        {
            var command = _fixture.Create<SubmitJobApplicationCommand>();
            var result = await _handler.Handle(command, CancellationToken.None);
            await _repository.Received(1).AddAsync(Arg.Any<JobApplication>(), Arg.Any<CancellationToken>());
            Assert.NotEqual(result, default);
        }
    }
}
