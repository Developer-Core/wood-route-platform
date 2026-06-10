namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get all orders received by a carpenter.
/// </summary>
public record GetOrdersByCarpenterIdQuery(int CarpenterId);
