using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;

/// <summary>
///     Base class for all Aggregate Roots in the WoodRoute Platform.
/// </summary>
/// <remarks>
///     An Aggregate Root is the entry point to an aggregate cluster of domain objects.
///     It guarantees the consistency of all changes made within the aggregate boundary
///     and is the only member of the aggregate that outside objects may hold references to.
///     <para>
///         Domain events raised during business logic execution are collected here and
///         dispatched by the infrastructure layer (the unit of work, after
///         <c>SaveChangesAsync</c>) rather than dispatched inline.
///     </para>
///     Example:
///     <code>
///     public class Order : AggregateRoot
///     {
///         public void Place()
///         {
///             // business logic ...
///             RaiseDomainEvent(new OrderPlacedEvent(Id, DateTimeOffset.UtcNow));
///         }
///     }
///     </code>
/// </remarks>
public abstract class AggregateRoot
{
    private readonly List<IEvent> _domainEvents = [];

    /// <summary>
    ///     Gets the read-only collection of domain events raised by this aggregate root.
    /// </summary>
    public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    ///     Raises a domain event and queues it for dispatching.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void RaiseDomainEvent(IEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    ///     Clears all domain events that have been collected.
    /// </summary>
    /// <remarks>
    ///     This method should be called by the infrastructure layer once all events
    ///     have been dispatched successfully.
    /// </remarks>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
