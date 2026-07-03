namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to validate a payment receipt, confirming or rejecting it.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order the payment belongs to.
/// </param>
/// <param name="PaymentId">
///     The identifier of the payment to validate.
/// </param>
/// <param name="IsApproved">
///     Whether the payment receipt is approved.
/// </param>
public record ValidatePaymentCommand(int OrderId, int PaymentId, bool IsApproved);
