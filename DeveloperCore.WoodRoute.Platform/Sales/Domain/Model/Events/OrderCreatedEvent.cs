using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;

/// <summary>
///     Domain event raised when a new order is created.
/// </summary>
public record OrderCreatedEvent(int OrderId, int CustomerId, int? CarpenterId, Guid PublicTrackingId) : IEvent;
