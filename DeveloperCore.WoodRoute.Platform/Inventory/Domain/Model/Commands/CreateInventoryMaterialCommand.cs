namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;

/// <summary>
///     Command to create a new inventory material.
/// </summary>
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
public record CreateInventoryMaterialCommand(
    string MaterialType,
    decimal Quantity,
    string Unit,
    decimal MinStock);