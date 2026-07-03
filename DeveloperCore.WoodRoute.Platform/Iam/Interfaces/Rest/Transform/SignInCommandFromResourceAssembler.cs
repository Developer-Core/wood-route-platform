using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="SignInResource" /> into a <see cref="SignInCommand" />.
/// </summary>
public static class SignInCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignInResource" /> to a <see cref="SignInCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="SignInResource" /> to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="SignInCommand" /> instance.
    /// </returns>
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}
