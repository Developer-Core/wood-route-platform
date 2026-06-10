using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;

/// <summary>
///     Order query service contract.
/// </summary>
public interface IOrderQueryService
{
    /// <summary>
    ///     Handles the get all orders query.
    /// </summary>
    Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get order by id query.
    /// </summary>
    Task<Order?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get order by public tracking id query.
    /// </summary>
    Task<Order?> Handle(GetOrderByPublicTrackingIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get orders by customer id query.
    /// </summary>
    Task<IEnumerable<Order>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get orders by carpenter id query.
    /// </summary>
    Task<IEnumerable<Order>> Handle(GetOrdersByCarpenterIdQuery query, CancellationToken cancellationToken = default);
}
