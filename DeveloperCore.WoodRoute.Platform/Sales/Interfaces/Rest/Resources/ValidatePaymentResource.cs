namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to validate a payment receipt, confirming or rejecting it.
/// </summary>
/// <param name="IsApproved">
///     Whether the payment receipt is approved.
/// </param>
public record ValidatePaymentResource(bool IsApproved);
