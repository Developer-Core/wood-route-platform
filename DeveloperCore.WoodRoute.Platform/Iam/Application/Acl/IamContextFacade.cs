using DeveloperCore.WoodRoute.Platform.Iam.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Acl;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.Acl;

/// <summary>
///     Anti-corruption layer facade implementation for the Iam bounded context.
/// </summary>
/// <param name="userCommandService">
///     User command service.
/// </param>
/// <param name="userQueryService">
///     User query service.
/// </param>
public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreateUser(string email, string password, EUserRole role,
        CancellationToken cancellationToken = default)
    {
        var signUpCommand = new SignUpCommand(string.Empty, string.Empty, email, password, role);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);
        return result.IsSuccess ? result.Value.user.Id : 0;
    }

    /// <inheritdoc />
    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByEmailQuery(email), cancellationToken);
        return user?.Id ?? 0;
    }

    /// <inheritdoc />
    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return user?.Email ?? string.Empty;
    }
}
