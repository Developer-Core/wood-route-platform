using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;

/// <summary>
///     Domain event raised when a customer accepts the quote of an order.
/// </summary>
public record QuoteAcceptedEvent(int OrderId, int QuoteId, decimal Total) : IEvent;
