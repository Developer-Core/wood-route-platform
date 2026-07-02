using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Handles read queries for the Manufacturing bounded context.
/// </summary>
public class ProductionQueryService(IManufactureOrderRepository manufactureOrderRepository)
    : IProductionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Stage>> Handle(GetStagesByOrderIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var order = await manufactureOrderRepository.FindBySalesOrderIdAsync(query.SalesOrderId, cancellationToken);
        // If there is no manufacture order yet, return empty instead of throwing
        return order?.Stages ?? [];
    }

    /// <inheritdoc />
    public async Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId,
        CancellationToken cancellationToken = default)
    {
        return await manufactureOrderRepository.FindBySalesOrderIdAsync(salesOrderId, cancellationToken);
    }
}
