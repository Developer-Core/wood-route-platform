namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Quote of an order for the REST API.
/// </summary>
public record QuoteResource(
    int Id,
    decimal MaterialsCost,
    decimal LaborCost,
    decimal Total,
    int EstimatedProductionDays,
    string Status);
