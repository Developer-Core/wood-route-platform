using DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.QueryServices;

/// <summary>
///     User query service implementation.
/// </summary>
/// <param name="userRepository">
///     User repository.
/// </param>
public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    /// <inheritdoc />
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken = default)
    {
        return await userRepository.FindByEmailAsync(query.Email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default)
    {
        return await userRepository.ListAsync(cancellationToken);
    }
}
