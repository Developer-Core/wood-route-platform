using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateCustomerResource" /> into a <see cref="CreateCustomerCommand" />.
/// </summary>
public static class CreateCustomerCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a create customer resource to its command representation.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateCustomerResource" /> containing the data for creating a customer.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateCustomerCommand" /> instance.
    /// </returns>
    public static CreateCustomerCommand ToCommandFromResource(CreateCustomerResource resource)
    {
        return new CreateCustomerCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            resource.UserId);
    }
}
