namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to accept a pending order.
/// </summary>
public record AcceptOrderCommand(int OrderId);
