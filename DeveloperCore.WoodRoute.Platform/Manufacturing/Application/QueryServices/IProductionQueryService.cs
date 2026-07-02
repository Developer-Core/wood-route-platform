using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;

/// <summary>
///     Production query service contract.
/// </summary>
public interface IProductionQueryService
{
    /// <summary>
    ///     Handles the get stages by order id query.
    /// </summary>
    Task<IEnumerable<Stage>> Handle(GetStagesByOrderIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the full manufacture order for a given sales order id, or null if not found.
    /// </summary>
    Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId, CancellationToken cancellationToken = default);
}
