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
    public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource)
    {
        return new CreateOrderCommand(
            resource.CustomerId,
            resource.CarpenterId,
            resource.FurnitureType,
            resource.Width,
            resource.Height,
            resource.Depth,
            resource.Material,
            resource.DesignNotes);
    }
}
