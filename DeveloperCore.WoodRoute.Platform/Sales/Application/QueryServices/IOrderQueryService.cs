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
    /// <param name="query">
    ///     The <see cref="GetAllOrdersQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A collection of all <see cref="Order" /> instances.
    /// </returns>
    Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get order by id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetOrderByIdQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Order" /> or null if none exists.
    /// </returns>
    Task<Order?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get order by public tracking id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetOrderByPublicTrackingIdQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Order" /> or null if none exists.
    /// </returns>
    Task<Order?> Handle(GetOrderByPublicTrackingIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get orders by customer id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetOrdersByCustomerIdQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Order" /> instances placed by the customer.
    /// </returns>
    Task<IEnumerable<Order>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get orders by carpenter id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetOrdersByCarpenterIdQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Order" /> instances received by the carpenter.
    /// </returns>
    Task<IEnumerable<Order>> Handle(GetOrdersByCarpenterIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get unassigned orders query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetUnassignedOrdersQuery" /> query
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The unassigned, pending <see cref="Order" /> instances that make up the pool.
    /// </returns>
    Task<IEnumerable<Order>> Handle(GetUnassignedOrdersQuery query, CancellationToken cancellationToken = default);
}
