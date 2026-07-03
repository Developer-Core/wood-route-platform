namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command to update the personal information of an existing user profile.
/// </summary>
/// <param name="ProfileId">
///     The identifier of the profile to update.
/// </param>
/// <param name="FirstName">
///     The first name of the profile.
/// </param>
/// <param name="LastName">
///     The last name of the profile.
/// </param>
/// <param name="Email">
///     The email address of the profile.
/// </param>
public record UpdateProfileCommand(
    int ProfileId,
    string FirstName,
    string LastName,
    string Email);
