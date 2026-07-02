namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the stock levels of an inventory material.
/// </summary>
public record UpdateInventoryMaterialResource(
    decimal Quantity,
    decimal MinStock);
