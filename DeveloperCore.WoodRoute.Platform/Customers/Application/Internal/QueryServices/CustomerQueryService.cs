using DeveloperCore.WoodRoute.Platform.Customers.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Customers.Application.Internal.QueryServices;

/// <summary>
///     Customer query service implementation.
/// </summary>
/// <param name="customerRepository">
///     Customer repository.
/// </param>
public class CustomerQueryService(ICustomerRepository customerRepository) : ICustomerQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await customerRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Customer?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await customerRepository.FindByIdAsync(query.CustomerId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Customer?> Handle(GetCustomerByUserIdQuery query, CancellationToken cancellationToken = default)
    {
        return await customerRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Customer?> Handle(GetCustomerByEmailQuery query, CancellationToken cancellationToken = default)
    {
        return await customerRepository.FindByEmailAsync(query.Email, cancellationToken);
    }
}
