using RecruitProApp.Domain.Common;

namespace RecruitProApp.Domain.ValueObjects
{
    /// <summary>An application score constrained to the 0-100 range.</summary>
    public sealed class Score : ValueObject
    {
        public int Value { get; }

        public Score(int value)
        {
            if (value is < 0 or > 100)
                throw new DomainException("Score must be between 0 and 100.");

            Value = value;
        }

        /// <summary>A strong score (&gt;= 70) preselects a candidate.</summary>
        public bool IsStrong => Value >= 70;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
