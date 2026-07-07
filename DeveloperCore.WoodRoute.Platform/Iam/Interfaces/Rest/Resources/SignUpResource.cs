namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource carrying the registration data for a public sign-up request (TS09).
/// </summary>
/// <remarks>
///     Public registration always creates a <c>Client</c> account. The role is forced
///     server-side and cannot be chosen by the caller; carpenter accounts are created through
///     the separate, invitation-gated carpenter sign-up endpoint.
/// </remarks>
/// <param name="FirstName">The first name of the user to register.</param>
/// <param name="LastName">The last name of the user to register.</param>
/// <param name="Email">The email address that will uniquely identify the user.</param>
/// <param name="Password">The plain text password of the user to register.</param>
public record SignUpResource(string FirstName, string LastName, string Email, string Password);
