namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to mark an in-progress order as ready for delivery.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to mark ready for delivery.
/// </param>
public record MarkOrderReadyCommand(int OrderId);
