using FluentAssertions;
using RecruitProApp.Domain.Common;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.JobApplications.Enums;
using RecruitProApp.Domain.Entities.JobApplications.Events;

namespace RecruitProApp.Tests.Domain.JobApplications
{
    public class JobApplicationTests
    {
        private static JobApplication NewApplication()
            => JobApplication.Submit(Guid.NewGuid(), Guid.NewGuid());

        [Fact]
        public void Submit_Should_Create_Pending_Application_And_Raise_Event()
        {
            var app = NewApplication();

            app.Status.Should().Be(JobApplicationStatus.PENDING);
            app.DomainEvents.Should().ContainSingle(e => e is JobApplicationSubmittedDomainEvent);
        }

        [Fact]
        public void Submit_Should_Throw_When_Offer_Is_Empty()
        {
            var act = () => JobApplication.Submit(Guid.Empty, Guid.NewGuid());

            act.Should().Throw<DomainException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void SetScore_Should_Reject_Out_Of_Range_Values(int score)
        {
            var app = NewApplication();

            var act = () => app.SetScore(score);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void SetScore_Should_Preselect_When_Score_Is_High()
        {
            var app = NewApplication();

            app.SetScore(85);

            app.Status.Should().Be(JobApplicationStatus.PRESELECTED);
        }

        [Fact]
        public void Accept_Should_Set_Status_And_Raise_Event()
        {
            var app = NewApplication();

            app.Accept();

            app.Status.Should().Be(JobApplicationStatus.ACCEPTED);
            app.DomainEvents.Should().Contain(e => e is JobApplicationAcceptedDomainEvent);
        }

        [Fact]
        public void Reject_Should_Require_A_Reason()
        {
            var app = NewApplication();

            var act = () => app.Reject("   ");

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Accept_Should_Fail_When_Application_Already_Rejected()
        {
            var app = NewApplication();
            app.Reject("Not a fit");

            var act = () => app.Accept();

            act.Should().Throw<DomainException>();
        }
    }
}
