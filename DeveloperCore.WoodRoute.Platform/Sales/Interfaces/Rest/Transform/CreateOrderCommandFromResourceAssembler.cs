using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateOrderResource" /> into a <see cref="CreateOrderCommand" />.
/// </summary>
public static class CreateOrderCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a create order resource to its command representation.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateOrderResource" /> containing the furniture data for creating an order.
    /// </param>
    /// <param name="customerId">
    ///     The resolved identifier of the customer placing the order.
    /// </param>
    /// <param name="carpenterId">
    ///     The identifier of the carpenter the order is assigned to, or <c>null</c> when it goes to the pool.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateOrderCommand" /> instance.
    /// </returns>
    public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource, int customerId,
        int? carpenterId)
    {
        return new CreateOrderCommand(
            customerId,
            carpenterId,
            resource.FurnitureType,
            resource.Width,
            resource.Height,
            resource.Depth,
            resource.Material,
            resource.DesignNotes);
    }
}
