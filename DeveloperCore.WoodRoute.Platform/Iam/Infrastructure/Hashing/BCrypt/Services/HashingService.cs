using DeveloperCore.WoodRoute.Platform.Iam.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;

/// <summary>
///     BCrypt-based implementation of the hashing service.
/// </summary>
public class HashingService : IHashingService
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}
