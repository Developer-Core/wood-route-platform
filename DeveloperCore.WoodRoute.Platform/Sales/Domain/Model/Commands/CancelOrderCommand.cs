namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to cancel a pending order.
/// </summary>
public record CancelOrderCommand(int OrderId);
