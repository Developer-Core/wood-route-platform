namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get an order by its public tracking id, used for unauthenticated order tracking.
/// </summary>
public record GetOrderByPublicTrackingIdQuery(Guid PublicTrackingId);
