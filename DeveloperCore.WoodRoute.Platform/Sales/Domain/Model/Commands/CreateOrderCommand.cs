namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to create a new custom furniture order.
/// </summary>
/// <remarks>
///     Dimensions are expressed in centimeters.
/// </remarks>
public record CreateOrderCommand(
    int CustomerId,
    int CarpenterId,
    string FurnitureType,
    decimal Width,
    decimal Height,
    decimal Depth,
    string Material,
    string DesignNotes);
