using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to register a payment for an order, pending receipt validation.
/// </summary>
public record RegisterPaymentResource(
    EPaymentType Type,
    decimal Amount,
    string ReceiptReference);
