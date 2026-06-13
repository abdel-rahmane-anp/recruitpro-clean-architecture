namespace RecruitProApp.Domain.Common
{
    /// <summary>Thrown when a domain invariant or business rule is violated.</summary>
    public sealed class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
