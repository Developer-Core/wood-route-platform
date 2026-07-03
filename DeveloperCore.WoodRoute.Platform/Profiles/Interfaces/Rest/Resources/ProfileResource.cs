namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Profile resource for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the profile.
/// </param>
/// <param name="FirstName">
///     The first name of the profile.
/// </param>
/// <param name="LastName">
///     The last name of the profile.
/// </param>
/// <param name="FullName">
///     The full name of the profile.
/// </param>
/// <param name="Email">
///     The email address of the profile.
/// </param>
public record ProfileResource(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email);
