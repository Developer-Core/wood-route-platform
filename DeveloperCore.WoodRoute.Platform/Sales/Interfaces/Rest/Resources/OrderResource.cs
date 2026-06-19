namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Order resource for the REST API.
/// </summary>
public record OrderResource(
    int Id,
    Guid PublicTrackingId,
    int CustomerId,
    int CarpenterId,
    string Status,
    FurnitureDetailsResource Details,
    QuoteResource? Quote,
    IEnumerable<PaymentResource> Payments);
