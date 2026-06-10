using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to register a payment for an order, pending receipt validation.
/// </summary>
public record RegisterPaymentCommand(
    int OrderId,
    EPaymentType Type,
    decimal Amount,
    string ReceiptReference);
