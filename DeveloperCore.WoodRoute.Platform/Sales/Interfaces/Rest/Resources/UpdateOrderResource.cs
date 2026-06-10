namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to modify the furniture details of a pending order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters and must be positive values.
/// </remarks>
public record UpdateOrderResource(
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
