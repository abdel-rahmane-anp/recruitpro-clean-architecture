using FluentAssertions;
using RecruitProApp.Domain.Common;
using RecruitProApp.Domain.ValueObjects;

namespace RecruitProApp.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("not-an-email")]
        [InlineData("missing@domain")]
        public void Constructor_Should_Reject_Invalid_Values(string value)
        {
            var act = () => new Email(value);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Constructor_Should_Accept_A_Valid_Email()
        {
            var email = new Email("  abdel@example.com  ");

            email.Value.Should().Be("abdel@example.com");
        }

        [Fact]
        public void Emails_With_Same_Value_Are_Equal_Ignoring_Case()
        {
            new Email("a@b.com").Should().Be(new Email("A@B.com"));
        }
    }
}
