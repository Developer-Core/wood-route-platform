namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get an order by its public tracking id, used for unauthenticated order tracking.
/// </summary>
/// <param name="PublicTrackingId">
///     The public tracking identifier of the order to get.
/// </param>
public record GetOrderByPublicTrackingIdQuery(Guid PublicTrackingId);
