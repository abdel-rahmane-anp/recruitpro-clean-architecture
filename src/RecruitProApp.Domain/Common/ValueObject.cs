namespace RecruitProApp.Domain.Common
{
    /// <summary>
    /// Base class for value objects: identity is structural (two value objects
    /// are equal when their components are equal), not by reference.
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
            => GetEqualityComponents()
                .Aggregate(0, (hash, component) => HashCode.Combine(hash, component));

        public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

        public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);
    }
}
