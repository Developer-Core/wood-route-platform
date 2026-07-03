namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to modify the furniture details of a pending order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters.
/// </remarks>
/// <param name="OrderId">
///     The identifier of the order to modify.
/// </param>
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
public record ModifyOrderCommand(
    int OrderId,
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
