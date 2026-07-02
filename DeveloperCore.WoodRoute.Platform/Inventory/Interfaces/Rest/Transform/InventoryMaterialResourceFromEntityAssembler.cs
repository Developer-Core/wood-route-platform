using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to transform an <see cref="InventoryMaterial" /> aggregate into an
///     <see cref="InventoryMaterialResource" />.
/// </summary>
public static class InventoryMaterialResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts an inventory material aggregate to its resource representation.
    /// </summary>
    public static InventoryMaterialResource ToResourceFromEntity(InventoryMaterial entity)
    {
        return new InventoryMaterialResource(
            entity.Id,
            entity.MaterialType,
            entity.Quantity,
            entity.Unit,
            entity.MinStock);
    }
}
