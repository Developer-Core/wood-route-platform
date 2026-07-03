namespace DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Rest.Resources;

/// <summary>
///     Quote of an order for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the quote.
/// </param>
/// <param name="MaterialsCost">
///     The cost of the materials.
/// </param>
/// <param name="LaborCost">
///     The cost of the labor.
/// </param>
/// <param name="Total">
///     The total cost of the quote.
/// </param>
/// <param name="EstimatedProductionDays">
///     The estimated number of production days.
/// </param>
/// <param name="Status">
///     The current status of the quote.
/// </param>
public record QuoteResource(
    int Id,
    decimal MaterialsCost,
    decimal LaborCost,
    decimal Total,
    int EstimatedProductionDays,
    string Status);
