namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to reject a pending order.
/// </summary>
public record RejectOrderCommand(int OrderId);
