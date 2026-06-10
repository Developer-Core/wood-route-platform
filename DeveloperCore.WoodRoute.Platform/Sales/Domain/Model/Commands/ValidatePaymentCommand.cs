namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to validate a payment receipt, confirming or rejecting it.
/// </summary>
public record ValidatePaymentCommand(int OrderId, int PaymentId, bool IsApproved);
