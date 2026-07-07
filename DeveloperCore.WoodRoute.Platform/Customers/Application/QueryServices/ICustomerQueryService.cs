using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Customers.Application.QueryServices;

/// <summary>
///     Customer query service contract.
/// </summary>
public interface ICustomerQueryService
{
    /// <summary>
    ///     Handles the get all customers query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetAllCustomersQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A collection of all <see cref="Customer" /> instances.
    /// </returns>
    Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get customer by id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetCustomerByIdQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Customer" /> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Customer?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get customer by user id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetCustomerByUserIdQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Customer" /> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Customer?> Handle(GetCustomerByUserIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get customer by email query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetCustomerByEmailQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Customer" /> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Customer?> Handle(GetCustomerByEmailQuery query, CancellationToken cancellationToken = default);
}
