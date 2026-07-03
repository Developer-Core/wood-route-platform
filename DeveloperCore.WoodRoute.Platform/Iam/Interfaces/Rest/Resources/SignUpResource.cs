using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource carrying the registration data for a sign-up request (TS09).
/// </summary>
/// <param name="Email">The email address that will uniquely identify the user.</param>
/// <param name="Password">The plain text password of the user to register.</param>
/// <param name="Role">The role chosen at registration.</param>
public record SignUpResource(string Email, string Password, EUserRole Role);
