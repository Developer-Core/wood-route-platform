namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Registration.Configuration;

/// <summary>
///     Strongly typed settings governing carpenter registration.
/// </summary>
/// <remarks>
///     Bound from the <c>Carpenter</c> section of <c>appsettings.json</c>. Carpenter sign-up is a
///     closed flow: only requests carrying the matching <see cref="InvitationCode" /> are allowed
///     to create a carpenter account.
/// </remarks>
public class CarpenterSettings
{
    /// <summary>
    ///     Gets or sets the invitation code required to register a carpenter account.
    /// </summary>
    public required string InvitationCode { get; set; }
}
