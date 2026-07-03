namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command to create a new user profile.
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
public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email);
