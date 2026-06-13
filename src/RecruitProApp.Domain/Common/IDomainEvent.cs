using MediatR;

namespace RecruitProApp.Domain.Common
{
    /// <summary>
    /// Marker interface for domain events. It extends MediatR's INotification so
    /// events can be dispatched through the existing MediatR pipeline once the
    /// unit of work is committed.
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
