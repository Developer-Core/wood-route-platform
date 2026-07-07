using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="User" /> aggregate into a <see cref="UserResource" />.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="User" /> aggregate to a <see cref="UserResource" />.
    /// </summary>
    /// <param name="user">
    ///     The <see cref="User" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="UserResource" /> instance.
    /// </returns>
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.FirstName, user.LastName, user.FullName, user.Email,
            user.Role.ToString());
    }
}
