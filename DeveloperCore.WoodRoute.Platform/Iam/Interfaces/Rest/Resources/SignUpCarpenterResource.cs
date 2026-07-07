namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource carrying the registration data for a carpenter sign-up request.
/// </summary>
/// <remarks>
///     Carpenter registration is closed: the request must supply a valid
///     <see cref="InvitationCode" />. The role is always set to <c>Carpenter</c> server-side.
/// </remarks>
/// <param name="FirstName">The first name of the carpenter to register.</param>
/// <param name="LastName">The last name of the carpenter to register.</param>
/// <param name="Email">The email address that will uniquely identify the carpenter.</param>
/// <param name="Password">The plain text password of the carpenter to register.</param>
/// <param name="InvitationCode">The invitation code that authorizes carpenter registration.</param>
public record SignUpCarpenterResource(string FirstName, string LastName, string Email, string Password, string InvitationCode);
