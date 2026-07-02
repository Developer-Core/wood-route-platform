namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;

/// <summary>
///     Command to create a new inventory material.
/// </summary>
public record CreateInventoryMaterialCommand(
    string MaterialType,
    decimal Quantity,
    string Unit,
    decimal MinStock);