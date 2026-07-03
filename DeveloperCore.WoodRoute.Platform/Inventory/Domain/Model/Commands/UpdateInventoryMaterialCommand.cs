namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;

/// <summary>
///     Command to update an existing inventory material.
/// </summary>
/// <param name="MaterialId">
///     The identifier of the material to update.
/// </param>
/// <param name="Quantity">
///     The new available quantity of the material.
/// </param>
/// <param name="MinStock">
///     The new minimum stock threshold for the material.
/// </param>
public record UpdateInventoryMaterialCommand(
    int MaterialId,
    decimal Quantity,
    decimal MinStock);