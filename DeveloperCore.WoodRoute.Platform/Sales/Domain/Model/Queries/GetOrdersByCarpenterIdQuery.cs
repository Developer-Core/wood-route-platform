namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get all orders received by a carpenter.
/// </summary>
/// <param name="CarpenterId">
///     The identifier of the carpenter.
/// </param>
public record GetOrdersByCarpenterIdQuery(int CarpenterId);
