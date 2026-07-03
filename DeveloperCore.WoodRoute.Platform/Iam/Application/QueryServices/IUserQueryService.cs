using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;

/// <summary>
///     User query service interface.
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    ///     Handles the get user by id query.
    /// </summary>
    /// <param name="query">The <see cref="GetUserByIdQuery" /> with the user id to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found, otherwise null.</returns>
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get user by email query.
    /// </summary>
    /// <param name="query">The <see cref="GetUserByEmailQuery" /> with the email to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found, otherwise null.</returns>
    Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get all users query.
    /// </summary>
    /// <param name="query">The <see cref="GetAllUsersQuery" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of users.</returns>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default);
}
