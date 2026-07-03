using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;

namespace DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Outbound port for JWT token generation and validation.
/// </summary>
public interface ITokenService
{
    /// <summary>
    ///     Generates a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <returns>The generated JWT token.</returns>
    string GenerateToken(User user);

    /// <summary>
    ///     Validates a JWT token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>The user identifier if the token is valid, otherwise null.</returns>
    Task<int?> ValidateToken(string token);
}
