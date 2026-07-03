namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource carrying the credentials for a sign-in request (TS01).
/// </summary>
/// <param name="Email">The email address of the user signing in.</param>
/// <param name="Password">The plain text password of the user signing in.</param>
public record SignInResource(string Email, string Password);
