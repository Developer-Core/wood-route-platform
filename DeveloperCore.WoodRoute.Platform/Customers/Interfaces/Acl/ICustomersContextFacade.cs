namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Acl;

/// <summary>
///     Anti-corruption facade exposing Customer resolution to other bounded contexts.
/// </summary>
public interface ICustomersContextFacade
{
    /// <summary>
    ///     Resolves the customer linked to the given account, creating one when none exists yet.
    /// </summary>
    /// <param name="userId">
    ///     The identifier of the authenticated account placing the order.
    /// </param>
    /// <param name="firstName">
    ///     The first name used when a new customer must be created.
    /// </param>
    /// <param name="lastName">
    ///     The last name used when a new customer must be created.
    /// </param>
    /// <param name="email">
    ///     The email address used when a new customer must be created.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The identifier of the existing or newly created customer.
    /// </returns>
    Task<int> FindOrCreateCustomerForUserAsync(int userId, string firstName, string lastName, string email,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Resolves the identifier of the customer linked to the given account, when any.
    /// </summary>
    /// <param name="userId">
    ///     The identifier of the authenticated account.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The customer identifier if a customer is linked to the account, otherwise <c>null</c>.
    /// </returns>
    Task<int?> FindCustomerIdByUserAsync(int userId, CancellationToken cancellationToken = default);
}
