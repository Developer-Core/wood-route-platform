namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to generate the quote for an order.
/// </summary>
public record GenerateQuoteCommand(
    int OrderId,
    decimal MaterialsCost,
    decimal LaborCost,
    int EstimatedProductionDays);
