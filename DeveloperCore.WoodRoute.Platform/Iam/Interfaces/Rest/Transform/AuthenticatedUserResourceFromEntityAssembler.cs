using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="User" /> aggregate and its JWT token into an
///     <see cref="AuthenticatedUserResource" />.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="User" /> aggregate and a JWT token to an <see cref="AuthenticatedUserResource" />.
    /// </summary>
    /// <param name="user">
    ///     The authenticated <see cref="User" /> aggregate.
    /// </param>
    /// <param name="token">
    ///     The JWT token issued for the authenticated user.
    /// </param>
    /// <returns>
    ///     A new <see cref="AuthenticatedUserResource" /> instance.
    /// </returns>
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.FirstName, user.LastName, user.FullName, user.Email,
            user.Role.ToString(), token);
    }
}
