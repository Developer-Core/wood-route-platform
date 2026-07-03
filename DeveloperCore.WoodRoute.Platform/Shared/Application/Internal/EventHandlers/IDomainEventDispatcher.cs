using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Shared.Application.Internal.EventHandlers;

/// <summary>
///     Dispatches domain events to their registered <c>IDomainEventHandler</c> instances.
/// </summary>
/// <remarks>
///     The dispatcher is invoked by the unit of work after changes are persisted. It resolves
///     every handler registered for the concrete runtime type of each event and awaits them in
///     turn, so a single event may be handled by zero, one, or many handlers.
/// </remarks>
public interface IDomainEventDispatcher
{
    /// <summary>
    ///     Dispatches the given domain events to their registered handlers.
    /// </summary>
    /// <param name="domainEvents">The domain events to dispatch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task DispatchAsync(IEnumerable<IEvent> domainEvents, CancellationToken cancellationToken = default);
}
