using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="UpdateProfileResource" /> into an <see cref="UpdateProfileCommand" />.
/// </summary>
public static class UpdateProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts an update profile resource to its command representation.
    /// </summary>
    /// <param name="profileId">
    ///     The identifier of the profile to update.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateProfileResource" /> containing the updated profile data.
    /// </param>
    /// <returns>
    ///     A new <see cref="UpdateProfileCommand" /> instance.
    /// </returns>
    public static UpdateProfileCommand ToCommandFromResource(int profileId, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            profileId,
            resource.FirstName,
            resource.LastName,
            resource.Email);
    }
}
