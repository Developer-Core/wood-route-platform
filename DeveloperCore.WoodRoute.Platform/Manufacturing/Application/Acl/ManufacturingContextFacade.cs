using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Acl;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Acl;

/// <summary>
///     Anti-corruption facade over the Manufacturing bounded context.
/// </summary>
/// <param name="manufactureOrderRepository">
///     Manufacture order repository
/// </param>
public class ManufacturingContextFacade(IManufactureOrderRepository manufactureOrderRepository)
    : IManufacturingContextFacade
{
    /// <inheritdoc />
    public async Task<(int Completed, int Total)> GetStageProgressAsync(int salesOrderId,
        CancellationToken cancellationToken = default)
    {
        var manufactureOrder =
            await manufactureOrderRepository.FindBySalesOrderIdAsync(salesOrderId, cancellationToken);
        if (manufactureOrder is null) return (0, 0);

        var completed = manufactureOrder.Stages.Count(s => s.Status == EStageStatus.Completed);
        return (completed, manufactureOrder.Stages.Count);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyDictionary<int, (int Completed, int Total)>> GetStageProgressForOrdersAsync(
        IEnumerable<int> salesOrderIds, CancellationToken cancellationToken = default)
    {
        var manufactureOrders =
            await manufactureOrderRepository.FindBySalesOrderIdsAsync(salesOrderIds, cancellationToken);

        return manufactureOrders.ToDictionary(
            mo => mo.SalesOrderId,
            mo => (mo.Stages.Count(s => s.Status == EStageStatus.Completed), mo.Stages.Count));
    }
}
