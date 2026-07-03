namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all production stages for a given sales order.
/// </summary>
public record GetStagesByOrderIdQuery(int SalesOrderId);
