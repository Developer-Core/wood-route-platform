namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

/// <summary>
///     Inventory material resource for the REST API.
/// </summary>
public record InventoryMaterialResource(
    int Id,
    string MaterialType,
    decimal Quantity,
    string Unit,
    decimal MinStock);
