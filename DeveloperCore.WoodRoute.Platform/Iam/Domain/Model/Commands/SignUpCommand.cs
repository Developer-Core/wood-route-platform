using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;

/// <summary>
///     Command to register a new user (TS09).
/// </summary>
/// <param name="Email">The email address that uniquely identifies the user.</param>
/// <param name="Password">The plain text password to be hashed before persistence.</param>
/// <param name="Role">The role chosen at registration.</param>
public record SignUpCommand(string Email, string Password, EUserRole Role);
