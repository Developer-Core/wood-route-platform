namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command to update the personal information of an existing user profile.
/// </summary>
public record UpdateProfileCommand(
    int ProfileId,
    string FirstName,
    string LastName,
    string Email);
