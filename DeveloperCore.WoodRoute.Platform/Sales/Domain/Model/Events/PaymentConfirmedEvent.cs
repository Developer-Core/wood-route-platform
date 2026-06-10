using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Events;

/// <summary>
///     Domain event raised when a payment receipt is validated and confirmed.
/// </summary>
public record PaymentConfirmedEvent(int OrderId, int PaymentId, EPaymentType Type, decimal Amount) : IEvent;
