namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Order resource for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the order.
/// </param>
/// <param name="PublicTrackingId">
///     The public tracking identifier of the order.
/// </param>
/// <param name="CustomerId">
///     The identifier of the customer who placed the order.
/// </param>
/// <param name="CarpenterId">
///     The identifier of the carpenter who received the order, or <c>null</c> while the order is
///     still in the unassigned pool.
/// </param>
/// <param name="Status">
///     The current status of the order.
/// </param>
/// <param name="Details">
///     The <see cref="FurnitureDetailsResource" /> of the order.
/// </param>
/// <param name="Quote">
///     The <see cref="QuoteResource" /> of the order, if any.
/// </param>
/// <param name="Payments">
///     The <see cref="PaymentResource" /> collection of the order.
/// </param>
/// <param name="CompletedStages">
///     The number of production stages already completed, or <c>0</c> when no plan exists.
/// </param>
/// <param name="TotalStages">
///     The total number of production stages, or <c>0</c> when no plan exists.
/// </param>
public record OrderResource(
    int Id,
    Guid PublicTrackingId,
    int CustomerId,
    int? CarpenterId,
    string Status,
    FurnitureDetailsResource Details,
    QuoteResource? Quote,
    IEnumerable<PaymentResource> Payments,
    int CompletedStages,
    int TotalStages);
