using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="UpdateOrderResource" /> into a <see cref="ModifyOrderCommand" />.
/// </summary>
public static class ModifyOrderCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts an update order resource and the target order id to its command representation.
    /// </summary>
    /// <param name="orderId">
    ///     The identifier of the order to modify.
    /// </param>
    /// <param name="resource">
    ///     The <see cref="UpdateOrderResource" /> containing the data for modifying the order.
    /// </param>
    /// <returns>
    ///     A new <see cref="ModifyOrderCommand" /> instance.
    /// </returns>
    public static ModifyOrderCommand ToCommandFromResource(int orderId, UpdateOrderResource resource)
    {
        return new ModifyOrderCommand(
            orderId,
            resource.FurnitureType,
            resource.Width,
            resource.Height,
            resource.Depth,
            resource.Material,
            resource.DesignNotes);
    }
}
