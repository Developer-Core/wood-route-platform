using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="UpdateCustomerResource" /> into an <see cref="UpdateCustomerCommand" />.
/// </summary>
public static class UpdateCustomerCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts an update customer resource to its command representation.
    /// </summary>
    /// <param name="customerId">
    ///     The identifier of the customer to update.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateCustomerResource" /> containing the updated customer data.
    /// </param>
    /// <returns>
    ///     A new <see cref="UpdateCustomerCommand" /> instance.
    /// </returns>
    public static UpdateCustomerCommand ToCommandFromResource(int customerId, UpdateCustomerResource resource)
    {
        return new UpdateCustomerCommand(
            customerId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone);
    }
}
