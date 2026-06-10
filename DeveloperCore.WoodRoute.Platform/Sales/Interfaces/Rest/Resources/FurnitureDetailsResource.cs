namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Furniture details of an order for the REST API.
/// </summary>
public record FurnitureDetailsResource(
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
