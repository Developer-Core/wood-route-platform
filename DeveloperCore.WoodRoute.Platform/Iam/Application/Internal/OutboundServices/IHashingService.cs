namespace DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Outbound port for password hashing and verification.
/// </summary>
public interface IHashingService
{
    /// <summary>
    ///     Hashes a plain text password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password.</returns>
    string HashPassword(string password);

    /// <summary>
    ///     Verifies a plain text password against a stored hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="passwordHash">The stored hash to verify against.</param>
    /// <returns>True if the password matches the hash, otherwise false.</returns>
    bool VerifyPassword(string password, string passwordHash);
}
