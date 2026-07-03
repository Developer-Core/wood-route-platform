namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to reject a pending order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to reject.
/// </param>
public record RejectOrderCommand(int OrderId);
