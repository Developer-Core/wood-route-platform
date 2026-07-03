using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform a <see cref="CreateInventoryMaterialResource" /> into a
///     <see cref="CreateInventoryMaterialCommand" />.
/// </summary>
public static class CreateInventoryMaterialCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a create inventory material resource to its command representation.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateInventoryMaterialResource" /> containing the data.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreateInventoryMaterialCommand" /> instance.
    /// </returns>
    public static CreateInventoryMaterialCommand ToCommandFromResource(CreateInventoryMaterialResource resource)
    {
        return new CreateInventoryMaterialCommand(
            resource.MaterialType,
            resource.Quantity,
            resource.Unit,
            resource.MinStock);
    }
}
