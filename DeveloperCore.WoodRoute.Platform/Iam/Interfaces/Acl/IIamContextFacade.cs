using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Acl;

/// <summary>
///     Anti-corruption layer facade exposing Iam operations to other bounded contexts.
/// </summary>
public interface IIamContextFacade
{
    /// <summary>
    ///     Creates a user and returns its identifier, or 0 when the creation fails.
    /// </summary>
    /// <param name="email">The email address of the user to create.</param>
    /// <param name="password">The plain text password of the user to create.</param>
    /// <param name="role">The role assigned to the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the created user, or 0 on failure.</returns>
    Task<int> CreateUser(string email, string password, EUserRole role, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetches the identifier of a user by its email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the user, or 0 when not found.</returns>
    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetches the email address of a user by its identifier.
    /// </summary>
    /// <param name="userId">The identifier of the user to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The email address of the user, or an empty string when not found.</returns>
    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken = default);
}
