using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;

/// <summary>
///     Order repository interface
/// </summary>
public interface IOrderRepository : IBaseRepository<Order>
{
    /// <summary>
    ///     Find an order by its public tracking id
    /// </summary>
    Task<Order?> FindByPublicTrackingIdAsync(Guid publicTrackingId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find all orders placed by a customer
    /// </summary>
    Task<IEnumerable<Order>> FindByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find all orders received by a carpenter
    /// </summary>
    Task<IEnumerable<Order>> FindByCarpenterIdAsync(int carpenterId, CancellationToken cancellationToken = default);
}
