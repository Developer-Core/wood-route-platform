namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;

/// <summary>
///     Command to update an existing inventory material.
/// </summary>
public record UpdateInventoryMaterialCommand(
    int MaterialId,
    decimal Quantity,
    decimal MinStock);