using FluentAssertions;
using RecruitProApp.Domain.Common;
using RecruitProApp.Domain.ValueObjects;

namespace RecruitProApp.Tests.Domain.ValueObjects
{
    public class ScoreTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void Constructor_Should_Reject_Out_Of_Range_Values(int value)
        {
            var act = () => new Score(value);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Constructor_Should_Accept_A_Valid_Value()
        {
            new Score(50).Value.Should().Be(50);
        }

        [Theory]
        [InlineData(70, true)]
        [InlineData(69, false)]
        public void IsStrong_Should_Reflect_The_Threshold(int value, bool expected)
        {
            new Score(value).IsStrong.Should().Be(expected);
        }
    }
}
