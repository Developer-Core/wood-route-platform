namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to modify the furniture details of a pending order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters.
/// </remarks>
public record ModifyOrderCommand(
    int OrderId,
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
