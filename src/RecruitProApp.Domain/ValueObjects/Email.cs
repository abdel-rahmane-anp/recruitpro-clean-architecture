using System.Text.RegularExpressions;
using RecruitProApp.Domain.Common;

namespace RecruitProApp.Domain.ValueObjects
{
    /// <summary>A validated email address. Cannot exist in an invalid state.</summary>
    public sealed class Email : ValueObject
    {
        private static readonly Regex Pattern =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email is required.");

            value = value.Trim();

            if (!Pattern.IsMatch(value))
                throw new DomainException($"'{value}' is not a valid email address.");

            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}
