namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;

/// <summary>
///     Defines a handler for a specific <see cref="IDomainEvent" />.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain event this handler processes.</typeparam>
/// <remarks>
///     Each handler is responsible for a single type of domain event, following the
///     Single Responsibility Principle.
///     <para>
///         Example implementation:
///         <code>
///         public class OrderPlacedDomainEventHandler : IDomainEventHandler&lt;OrderPlacedDomainEvent&gt;
///         {
///             public Task HandleAsync(OrderPlacedDomainEvent domainEvent, CancellationToken cancellationToken)
///             {
///                 // handle the event...
///                 return Task.CompletedTask;
///             }
///         }
///         </code>
///     </para>
/// </remarks>
public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
{
    /// <summary>
    ///     Handles the given domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
