using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="SignUpResource" /> into a <see cref="SignUpCommand" />.
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignUpResource" /> to a <see cref="SignUpCommand" />.
    /// </summary>
    /// <remarks>
    ///     The role is forced to <see cref="EUserRole.Client" /> server-side: public registration
    ///     can never create a carpenter account.
    /// </remarks>
    /// <param name="resource">
    ///     The <see cref="SignUpResource" /> to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="SignUpCommand" /> instance.
    /// </returns>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.FirstName, resource.LastName, resource.Email, resource.Password, EUserRole.Client);
    }
}
