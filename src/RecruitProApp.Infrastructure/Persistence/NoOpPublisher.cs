using MediatR;

namespace RecruitProApp.Infrastructure.Persistence
{
    /// <summary>
    /// No-op publisher used only by the design-time DbContext factory
    /// (EF Core migrations), where MediatR is not available.
    /// </summary>
    internal sealed class NoOpPublisher : IPublisher
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
            => Task.CompletedTask;
    }
}
