using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="SignUpCarpenterResource" /> into a
///     <see cref="SignUpCarpenterCommand" />.
/// </summary>
public static class SignUpCarpenterCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignUpCarpenterResource" /> to a <see cref="SignUpCarpenterCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="SignUpCarpenterResource" /> to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="SignUpCarpenterCommand" /> instance.
    /// </returns>
    public static SignUpCarpenterCommand ToCommandFromResource(SignUpCarpenterResource resource)
    {
        return new SignUpCarpenterCommand(resource.Email, resource.Password, resource.InvitationCode);
    }
}
