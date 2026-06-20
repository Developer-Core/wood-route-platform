namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to validate a payment receipt, confirming or rejecting it.
/// </summary>
public record ValidatePaymentResource(bool IsApproved);
