using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="Customer" /> aggregate into a <see cref="CustomerResource" />.
/// </summary>
public static class CustomerResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a customer aggregate to its resource representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="Customer" /> aggregate to convert.
    /// </param>
    /// <returns>
    ///     A new <see cref="CustomerResource" /> instance.
    /// </returns>
    public static CustomerResource ToResourceFromEntity(Customer entity)
    {
        return new CustomerResource(
            entity.Id,
            entity.Name.FirstName,
            entity.Name.LastName,
            entity.FullName,
            entity.EmailAddress,
            entity.Phone,
            entity.UserId);
    }
}
