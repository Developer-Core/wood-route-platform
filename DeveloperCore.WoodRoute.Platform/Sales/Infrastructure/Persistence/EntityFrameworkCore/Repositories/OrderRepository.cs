using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Order repository implementation.
/// </summary>
public class OrderRepository(AppDbContext context) : BaseRepository<Order>(context), IOrderRepository
{
    /// <inheritdoc />
    public async Task<Order?> FindByPublicTrackingIdAsync(Guid publicTrackingId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Order>()
            .Include(order => order.Quote)
            .Include(order => order.Payments)
            .FirstOrDefaultAsync(order => order.PublicTrackingId == publicTrackingId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> FindByCustomerIdAsync(int customerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Order>()
            .Include(order => order.Quote)
            .Include(order => order.Payments)
            .Where(order => order.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> FindByCarpenterIdAsync(int carpenterId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Order>()
            .Include(order => order.Quote)
            .Include(order => order.Payments)
            .Where(order => order.CarpenterId == carpenterId)
            .ToListAsync(cancellationToken);
    }
}
