using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices.Queries;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Contract for the production query service.
/// </summary>
public interface IProductionQueryService
{
    /// <summary>
    ///     Returns all production stages for a given sales order.
    ///     Returns an empty collection if no manufacture order has been created yet.
    /// </summary>
    Task<IEnumerable<Stage>> Handle(GetStagesByOrderIdQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the full manufacture order for a given sales order id, or null if not found.
    /// </summary>
    Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId,
        CancellationToken cancellationToken = default);
}
