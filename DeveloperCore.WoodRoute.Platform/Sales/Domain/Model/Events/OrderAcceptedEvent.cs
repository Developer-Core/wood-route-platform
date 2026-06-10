using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;

/// <summary>
///     Domain event raised when a carpenter accepts an order.
/// </summary>
public record OrderAcceptedEvent(int OrderId) : IEvent;
