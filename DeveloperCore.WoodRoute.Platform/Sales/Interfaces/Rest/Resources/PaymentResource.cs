namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Payment of an order for the REST API.
/// </summary>
public record PaymentResource(
    int Id,
    string Type,
    decimal Amount,
    string ReceiptReference,
    string Status);
