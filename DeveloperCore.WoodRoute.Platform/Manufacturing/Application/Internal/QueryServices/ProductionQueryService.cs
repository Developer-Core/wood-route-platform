using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Production query service implementation.
/// </summary>
public class ProductionQueryService(IManufactureOrderRepository manufactureOrderRepository)
    : IProductionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Stage>> Handle(GetStagesByOrderIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var order = await manufactureOrderRepository.FindBySalesOrderIdAsync(query.SalesOrderId, cancellationToken);
        return order?.Stages ?? [];
    }
}
