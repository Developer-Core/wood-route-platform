using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="UpdateInventoryMaterialResource" /> into an
///     <see cref="UpdateInventoryMaterialCommand" />.
/// </summary>
public static class UpdateInventoryMaterialCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts an update inventory material resource and the target material id to its command representation.
    /// </summary>
    public static UpdateInventoryMaterialCommand ToCommandFromResource(int materialId,
        UpdateInventoryMaterialResource resource)
    {
        return new UpdateInventoryMaterialCommand(
            materialId,
            resource.Quantity,
            resource.MinStock);
    }
}
