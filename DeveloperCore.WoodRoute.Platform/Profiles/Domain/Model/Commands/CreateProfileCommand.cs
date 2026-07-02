namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command to create a new user profile.
/// </summary>
public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email);
