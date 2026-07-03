namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to modify the furniture details of a pending order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters and must be positive values.
/// </remarks>
/// <param name="FurnitureType">
///     The type of furniture requested.
/// </param>
/// <param name="Width">
///     The width of the furniture piece, in centimeters.
/// </param>
/// <param name="Height">
///     The height of the furniture piece, in centimeters.
/// </param>
/// <param name="Depth">
///     The depth of the furniture piece, in centimeters.
/// </param>
/// <param name="Material">
///     The material requested for the furniture piece.
/// </param>
/// <param name="DesignNotes">
///     Additional design notes for the furniture piece.
/// </param>
public record UpdateOrderResource(
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
