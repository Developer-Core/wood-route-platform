using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;

/// <summary>
/// Domain errors for the Inventory bounded context.
/// </summary>
public static class InventoryErrors
{
    public static readonly Error MaterialNotFound =
        new("Inventory.MaterialNotFound", "The specified inventory material was not found.");

    public static readonly Error InvalidQuantity =
        new("Inventory.InvalidQuantity", "Quantity must be greater than zero.");

    public static readonly Error InvalidMinimumStock =
        new("Inventory.InvalidMinimumStock", "Minimum stock must be greater than or equal to zero.");
}