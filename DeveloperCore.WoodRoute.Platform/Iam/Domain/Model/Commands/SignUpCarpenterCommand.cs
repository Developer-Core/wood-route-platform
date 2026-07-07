namespace DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;

/// <summary>
///     Command to register a new carpenter through the closed, invitation-gated flow.
/// </summary>
/// <remarks>
///     The role is fixed to <c>Carpenter</c> server-side; the request cannot choose it. The
///     <see cref="InvitationCode" /> is validated against the configured carpenter invitation code
///     before the account is created.
/// </remarks>
/// <param name="FirstName">The first name of the carpenter.</param>
/// <param name="LastName">The last name of the carpenter.</param>
/// <param name="Email">The email address that uniquely identifies the carpenter.</param>
/// <param name="Password">The plain text password to be hashed before persistence.</param>
/// <param name="InvitationCode">The invitation code that authorizes carpenter registration.</param>
public record SignUpCarpenterCommand(string FirstName, string LastName, string Email, string Password, string InvitationCode);
