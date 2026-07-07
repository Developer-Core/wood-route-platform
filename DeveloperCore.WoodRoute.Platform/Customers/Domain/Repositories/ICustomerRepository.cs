using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Repositories;

/// <summary>
///     Customer repository interface.
/// </summary>
public interface ICustomerRepository : IBaseRepository<Customer>
{
    /// <summary>
    ///     Find a customer by its email address.
    /// </summary>
    Task<Customer?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Find a customer by the account it is linked to.
    /// </summary>
    Task<Customer?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
