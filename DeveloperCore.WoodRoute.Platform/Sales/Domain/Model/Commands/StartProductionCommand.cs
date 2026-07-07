namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to start the production of an accepted order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to start producing.
/// </param>
public record StartProductionCommand(int OrderId);
