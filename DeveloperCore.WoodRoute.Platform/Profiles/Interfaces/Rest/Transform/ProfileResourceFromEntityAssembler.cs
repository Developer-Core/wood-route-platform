using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="Profile" /> aggregate into a <see cref="ProfileResource" />.
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a profile aggregate to its resource representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Profile" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="ProfileResource" /> instance.
    /// </returns>
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id,
            entity.Name.FirstName,
            entity.Name.LastName,
            entity.FullName,
            entity.EmailAddress);
    }
}
