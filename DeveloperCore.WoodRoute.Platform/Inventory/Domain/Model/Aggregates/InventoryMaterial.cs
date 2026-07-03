using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Events;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;

/// <summary>
///     Inventory Material Aggregate Root
/// </summary>
/// <remarks>
///     Tracks the stock of a raw material managed by the workshop. Stock updates are guarded
///     and return a domain <see cref="Error" /> (<see cref="Error.None" /> on success). A
///     <see cref="LowStockDetectedEvent" /> is raised whenever an update leaves the available
///     quantity below the configured minimum stock.
/// </remarks>
public class InventoryMaterial : AggregateRoot, IAuditableEntity
{
    private InventoryMaterial()
    {
        MaterialType = string.Empty;
        Unit = string.Empty;
    }

    public InventoryMaterial(CreateInventoryMaterialCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.MaterialType))
            throw new ArgumentException("Material type is required.", nameof(command));
        if (string.IsNullOrWhiteSpace(command.Unit))
            throw new ArgumentException("Unit is required.", nameof(command));
        if (command.Quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(command), "Quantity must be a positive value.");
        if (command.MinStock < 0)
            throw new ArgumentOutOfRangeException(nameof(command), "Minimum stock cannot be negative.");
        MaterialType = command.MaterialType;
        Quantity = command.Quantity;
        Unit = command.Unit;
        MinStock = command.MinStock;
        RaiseDomainEvent(new InventoryMaterialCreatedEvent(Id));
    }

    public int Id { get; }
    public string MaterialType { get; private set; }
    public decimal Quantity { get; private set; }
    public string Unit { get; private set; }
    public decimal MinStock { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the available quantity and minimum stock of the material.
    /// </summary>
    public Error Update(UpdateInventoryMaterialCommand command)
    {
        if (command.Quantity <= 0) return InventoryErrors.InvalidQuantity;
        if (command.MinStock < 0) return InventoryErrors.InvalidMinimumStock;
        Quantity = command.Quantity;
        MinStock = command.MinStock;
        if (Quantity < MinStock) RaiseDomainEvent(new LowStockDetectedEvent(Id));
        return Error.None;
    }
}
