namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the personal information of a user profile.
/// </summary>
/// <param name="FirstName">
///     The first name of the profile.
/// </param>
/// <param name="LastName">
///     The last name of the profile.
/// </param>
/// <param name="Email">
///     The email address of the profile.
/// </param>
public record UpdateProfileResource(
    string FirstName,
    string LastName,
    string Email);
