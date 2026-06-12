namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;

public static class InventoryErrors
{
    public const string MaterialNotFound = "Inventory material was not found.";
    public const string InvalidQuantity = "Quantity must be greater than zero.";
    public const string InvalidMinimumStock = "Minimum stock must be greater than or equal to zero.";
}