namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to create a new custom furniture order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters and must be positive values.
/// </remarks>
public record CreateOrderResource(
    int CustomerId,
    int CarpenterId,
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
