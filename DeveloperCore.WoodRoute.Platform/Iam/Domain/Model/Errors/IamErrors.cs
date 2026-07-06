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

    public static readonly Error InsufficientRole =
        new("Iam.InsufficientRole", "The current user role is not allowed to perform this action.");

    public static readonly Error InvalidInvitationCode =
        new("Iam.InvalidInvitationCode", "The provided carpenter invitation code is invalid.");
}
