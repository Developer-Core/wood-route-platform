namespace DeveloperCore.WoodRoute.Platform.Inventory.Interfaces.Rest.Resources;

/// <summary>
///     Inventory material resource for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the material.
/// </param>
/// <param name="MaterialType">
///     The type of the material.
/// </param>
/// <param name="Quantity">
///     The available quantity of the material.
/// </param>
/// <param name="Unit">
///     The unit of measurement for the material.
/// </param>
/// <param name="MinStock">
///     The minimum stock threshold for the material.
/// </param>
public record InventoryMaterialResource(
    int Id,
    string MaterialType,
    decimal Quantity,
    string Unit,
    decimal MinStock);
