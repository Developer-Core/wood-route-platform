namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

/// <summary>
///     Resource to register a new inventory material.
/// </summary>
public record CreateInventoryMaterialResource(
    string MaterialType,
    decimal Quantity,
    string Unit,
    decimal MinStock);
