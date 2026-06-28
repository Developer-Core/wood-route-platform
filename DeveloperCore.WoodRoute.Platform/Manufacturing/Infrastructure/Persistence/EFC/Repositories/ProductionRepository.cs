using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     EF Core implementation of <see cref="IManufactureOrderRepository" />.
/// </summary>
public class ProductionRepository(AppDbContext context)
    : BaseRepository<ManufactureOrder>(context), IManufactureOrderRepository
{
    /// <inheritdoc />
    public async Task<ManufactureOrder?> FindBySalesOrderIdAsync(int salesOrderId,
        CancellationToken cancellationToken = default)
    {
        // Include stages so the aggregate has all child entities loaded —
        // we need them to run business logic (UpdateStageStatus, DefineStages).
        return await Context.Set<ManufactureOrder>()
            .Include(mo => mo.Stages)
            .FirstOrDefaultAsync(mo => mo.SalesOrderId == salesOrderId, cancellationToken);
    }
}
