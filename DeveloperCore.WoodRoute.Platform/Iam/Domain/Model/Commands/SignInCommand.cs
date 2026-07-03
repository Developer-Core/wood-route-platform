namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;

/// <summary>
///     Command to authenticate an existing user (TS01).
/// </summary>
/// <param name="Email">The email address of the user signing in.</param>
/// <param name="Password">The plain text password to verify against the stored hash.</param>
public record SignInCommand(string Email, string Password);
