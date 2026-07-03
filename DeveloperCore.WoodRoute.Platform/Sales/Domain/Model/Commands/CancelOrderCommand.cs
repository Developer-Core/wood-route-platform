namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to cancel a pending order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to cancel.
/// </param>
public record CancelOrderCommand(int OrderId);
