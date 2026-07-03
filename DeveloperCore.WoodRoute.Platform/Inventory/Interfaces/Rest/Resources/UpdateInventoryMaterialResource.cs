namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the stock levels of an inventory material.
/// </summary>
/// <param name="Quantity">
///     The new available quantity of the material.
/// </param>
/// <param name="MinStock">
///     The new minimum stock threshold for the material.
/// </param>
public record UpdateInventoryMaterialResource(
    decimal Quantity,
    decimal MinStock);
