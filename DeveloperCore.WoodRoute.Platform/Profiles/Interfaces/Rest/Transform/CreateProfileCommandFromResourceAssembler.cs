using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateProfileResource" /> into a <see cref="CreateProfileCommand" />.
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a create profile resource to its command representation.
    /// </summary>
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email);
    }
}
