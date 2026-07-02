namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource to create a new user profile.
/// </summary>
public record CreateProfileResource(
    string FirstName,
    string LastName,
    string Email);
