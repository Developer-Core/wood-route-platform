namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get an order by its id.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to get.
/// </param>
public record GetOrderByIdQuery(int OrderId);
