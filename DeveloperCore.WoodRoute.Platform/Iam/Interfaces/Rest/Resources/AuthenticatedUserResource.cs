namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource representing an authenticated user together with its JWT token.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
/// <param name="FullName">The full name of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Role">The role of the user.</param>
/// <param name="Token">The JWT token issued for the user.</param>
public record AuthenticatedUserResource(int Id, string FirstName, string LastName, string FullName, string Email,
    string Role, string Token);
