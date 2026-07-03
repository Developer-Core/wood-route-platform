using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to register a payment for an order, pending receipt validation.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order the payment belongs to.
/// </param>
/// <param name="Type">
///     The <see cref="EPaymentType" /> of the payment.
/// </param>
/// <param name="Amount">
///     The amount of the payment.
/// </param>
/// <param name="ReceiptReference">
///     The reference of the payment receipt.
/// </param>
public record RegisterPaymentCommand(
    int OrderId,
    EPaymentType Type,
    decimal Amount,
    string ReceiptReference);
