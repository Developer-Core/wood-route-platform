namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Resource to generate the quote for a pending order.
/// </summary>
/// <remarks>
///     Costs cannot be negative, the total must be positive and the estimated production days must be positive.
/// </remarks>
public record GenerateQuoteResource(
    decimal MaterialsCost,
    decimal LaborCost,
    int EstimatedProductionDays);
