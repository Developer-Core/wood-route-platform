namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the personal information of a user profile.
/// </summary>
public record UpdateProfileResource(
    string FirstName,
    string LastName,
    string Email);
