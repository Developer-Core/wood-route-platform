namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

/// <summary>
///     Strongly typed settings for JWT token generation and validation.
/// </summary>
/// <remarks>
///     Bound from the <c>TokenSettings</c> section of <c>appsettings.json</c>.
/// </remarks>
public class TokenSettings
{
    /// <summary>
    ///     Gets or sets the secret used to sign and validate JWT tokens.
    /// </summary>
    public required string Secret { get; set; }
}
