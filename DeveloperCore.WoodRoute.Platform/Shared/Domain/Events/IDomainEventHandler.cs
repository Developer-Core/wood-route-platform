using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;

/// <summary>
///     Defines a handler for a specific domain <see cref="IEvent" />.
/// </summary>
/// <typeparam name="TEvent">The type of domain event this handler processes.</typeparam>
/// <remarks>
///     Each handler is responsible for a single type of domain event, following the
///     Single Responsibility Principle. Handlers are resolved from the dependency
///     injection container and invoked by the <c>IDomainEventDispatcher</c>.
///     <para>
///         Example implementation:
///         <code>
///         public class OrderPlacedEventHandler : IDomainEventHandler&lt;OrderPlacedEvent&gt;
///         {
///             public Task HandleAsync(OrderPlacedEvent domainEvent, CancellationToken cancellationToken)
///             {
///                 // handle the event...
///                 return Task.CompletedTask;
///             }
///         }
///         </code>
///     </para>
/// </remarks>
public interface IDomainEventHandler<in TEvent> where TEvent : IEvent
{
    /// <summary>
    ///     Handles the given domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
