using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Iam bounded context.
/// </summary>
public static class IamErrors
{
    public static readonly Error InvalidCredentials =
        new("Iam.InvalidCredentials", "Invalid email or password.");

    public static readonly Error EmailAlreadyRegistered =
        new("Iam.EmailAlreadyRegistered", "The specified email is already registered.");

    public static readonly Error UserNotFound =
        new("Iam.UserNotFound", "The specified user was not found.");
}
