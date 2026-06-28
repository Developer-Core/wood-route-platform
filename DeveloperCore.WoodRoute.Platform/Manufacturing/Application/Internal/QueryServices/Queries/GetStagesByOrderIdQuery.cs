namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices.Queries;

/// <summary>
///     Query to retrieve all production stages for a given sales order.
/// </summary>
/// <param name="SalesOrderId">Id of the sales order whose stages we want to list.</param>
public record GetStagesByOrderIdQuery(int SalesOrderId);
