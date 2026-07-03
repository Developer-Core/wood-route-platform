using DeveloperCore.WoodRoute.Platform.Iam.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.CommandServices;

/// <summary>
///     User command service implementation.
/// </summary>
/// <param name="userRepository">
///     User repository.
/// </param>
/// <param name="hashingService">
///     Hashing service used to hash and verify passwords.
/// </param>
/// <param name="tokenService">
///     Token service used to issue JWT tokens.
/// </param>
/// <param name="unitOfWork">
///     Unit of work.
/// </param>
public class UserCommandService(
    IUserRepository userRepository,
    IHashingService hashingService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    /// <inheritdoc />
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamErrors.InvalidCredentials);

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }

    /// <inheritdoc />
    public async Task<Result<(User user, string token)>> Handle(SignUpCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result<(User user, string token)>.Failure(IamErrors.EmailAlreadyRegistered);

        var passwordHash = hashingService.HashPassword(command.Password);
        var user = new User(command, passwordHash);
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }
}
