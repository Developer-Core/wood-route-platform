using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;

/// <summary>
///     Inventory Material Aggregate Root
/// </summary>

public class InventoryMaterial : IAuditableEntity
{
    private InventoryMaterial()
    {
        MaterialType = string.Empty;
        Unit = string.Empty;
    }

    public InventoryMaterial(CreateInventoryMaterialCommand command)
    {
        MaterialType = command.MaterialType;
        Quantity = command.Quantity;
        Unit = command.Unit;
        MinStock = command.MinStock;
    }

    public int Id { get; }

    public string MaterialType { get; private set; }

    public decimal Quantity { get; private set; }

    public string Unit { get; private set; }

    public decimal MinStock { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}