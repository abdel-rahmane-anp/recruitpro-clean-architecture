namespace RecruitProApp.Domain.Common
{
    /// <summary>
    /// Base class for aggregate roots. It records the domain events raised during
    /// the aggregate's lifetime so the infrastructure can publish them after the
    /// changes are persisted.
    /// </summary>
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
