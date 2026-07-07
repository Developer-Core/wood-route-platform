using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Manufacture order repository implementation.
/// </summary>
public class ManufactureOrderRepository(AppDbContext context)
    : BaseRepository<ManufactureOrder>(context), IManufactureOrderRepository
{
    /// <inheritdoc />
    public async Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ManufactureOrder>()
            .Include(mo => mo.Stages)
            .FirstOrDefaultAsync(mo => mo.SalesOrderId == salesOrderId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ManufactureOrder>> FindBySalesOrderIdsAsync(IEnumerable<int> salesOrderIds,
        CancellationToken cancellationToken = default)
    {
        var ids = salesOrderIds.Distinct().ToList();
        if (ids.Count == 0) return [];

        return await Context.Set<ManufactureOrder>()
            .Include(mo => mo.Stages)
            .Where(mo => ids.Contains(mo.SalesOrderId))
            .ToListAsync(cancellationToken);
    }
}
