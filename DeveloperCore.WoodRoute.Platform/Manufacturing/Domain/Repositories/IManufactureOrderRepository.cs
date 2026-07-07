using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository contract for the <see cref="ManufactureOrder" /> aggregate.
/// </summary>
public interface IManufactureOrderRepository : IBaseRepository<ManufactureOrder>
{
    /// <summary>
    ///     Finds the manufacture order linked to a specific sales order id.
    ///     Includes all stages so the aggregate can run its business logic.
    /// </summary>
    Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds the manufacture orders linked to the given sales order ids in a single query.
    ///     Includes all stages so callers can compute progress without extra round-trips.
    /// </summary>
    Task<IReadOnlyList<ManufactureOrder>> FindBySalesOrderIdsAsync(IEnumerable<int> salesOrderIds,
        CancellationToken cancellationToken = default);
}
