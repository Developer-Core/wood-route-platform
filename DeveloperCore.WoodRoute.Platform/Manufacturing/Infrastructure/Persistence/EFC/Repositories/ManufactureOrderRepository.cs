using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EFC.Repositories;

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
}
