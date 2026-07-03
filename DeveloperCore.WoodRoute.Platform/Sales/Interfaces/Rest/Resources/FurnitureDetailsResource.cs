namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Furniture details of an order for the REST API.
/// </summary>
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
public record FurnitureDetailsResource(
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
