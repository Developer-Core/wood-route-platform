namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to generate the quote for an order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to quote.
/// </param>
/// <param name="MaterialsCost">
///     The cost of the materials.
/// </param>
/// <param name="LaborCost">
///     The cost of the labor.
/// </param>
/// <param name="EstimatedProductionDays">
///     The estimated number of production days.
/// </param>
public record GenerateQuoteCommand(
    int OrderId,
    decimal MaterialsCost,
    decimal LaborCost,
    int EstimatedProductionDays);
