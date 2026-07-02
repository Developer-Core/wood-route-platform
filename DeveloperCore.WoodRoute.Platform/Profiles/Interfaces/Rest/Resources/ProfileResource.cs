namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Profile resource for the REST API.
/// </summary>
public record ProfileResource(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email);
