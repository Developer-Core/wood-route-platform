namespace DeveloperCore.WoodRoute.Platform.Shared.Domain.Events;

/// <summary>
///     Marker interface for all domain events in the WoodRoute Platform.
/// </summary>
/// <remarks>
///     A domain event represents something significant that happened within a bounded context.
///     Events are raised by Aggregate Roots and should be named in the past tense,
///     for example: <c>OrderPlacedDomainEvent</c>, <c>UserRegisteredDomainEvent</c>.
///     <para>
///         Implement this interface on a <c>record</c> to get value equality for free:
///         <code>
///         public record OrderPlacedDomainEvent(int OrderId, DateTimeOffset OccurredOn) : IDomainEvent;
///         </code>
///     </para>
/// </remarks>
public interface IDomainEvent
{
    /// <summary>
    ///     Gets the UTC timestamp when the domain event occurred.
    /// </summary>
    DateTimeOffset OccurredOn => DateTimeOffset.UtcNow;
}
