using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to register a payment for an order, pending receipt validation.
/// </summary>
/// <param name="Type">
///     The <see cref="EPaymentType" /> of the payment.
/// </param>
/// <param name="Amount">
///     The amount of the payment.
/// </param>
/// <param name="ReceiptReference">
///     The reference of the payment receipt.
/// </param>
public record RegisterPaymentResource(
    EPaymentType Type,
    decimal Amount,
    string ReceiptReference);
