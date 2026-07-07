using DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Sales.Application.Internal.QueryServices;

/// <summary>
///     Order query service implementation.
/// </summary>
/// <param name="orderRepository">
///     Order repository
/// </param>
public class OrderQueryService(IOrderRepository orderRepository) : IOrderQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await orderRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Order?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await orderRepository.FindByIdAsync(query.OrderId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Order?> Handle(GetOrderByPublicTrackingIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await orderRepository.FindByPublicTrackingIdAsync(query.PublicTrackingId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetOrdersByCustomerIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await orderRepository.FindByCustomerIdAsync(query.CustomerId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetOrdersByCarpenterIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await orderRepository.FindByCarpenterIdAsync(query.CarpenterId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetUnassignedOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await orderRepository.FindUnassignedAsync(cancellationToken);
    }
}
