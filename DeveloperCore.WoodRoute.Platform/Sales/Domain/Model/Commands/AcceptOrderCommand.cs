namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to accept a pending order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to accept.
/// </param>
public record AcceptOrderCommand(int OrderId);
