using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Repositories;

/// <summary>
///     User repository interface.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Find a user by its email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found, otherwise null.</returns>
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Check whether a user exists for the given email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if a user exists, otherwise false.</returns>
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}
