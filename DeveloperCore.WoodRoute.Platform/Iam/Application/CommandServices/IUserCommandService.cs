using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.CommandServices;

/// <summary>
///     User command service interface.
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    ///     Handles the sign-in command, authenticating the user and issuing a JWT token (TS01).
    /// </summary>
    /// <param name="command">The <see cref="SignInCommand" /> with the credentials to validate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result wrapping the authenticated user and its JWT token.</returns>
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the sign-up command, registering the user and issuing a JWT token (TS09).
    /// </summary>
    /// <param name="command">The <see cref="SignUpCommand" /> with the registration data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result wrapping the created user and its JWT token.</returns>
    Task<Result<(User user, string token)>> Handle(SignUpCommand command, CancellationToken cancellationToken = default);
}
