using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Profiles bounded context.
/// </summary>
public static class ProfileErrors
{
    public static readonly Error ProfileNotFound =
        new("Profiles.ProfileNotFound", "The specified profile was not found.");

    public static readonly Error EmailAlreadyRegistered =
        new("Profiles.EmailAlreadyRegistered", "A profile with the specified email address already exists.");
}
