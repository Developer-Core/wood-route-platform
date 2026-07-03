namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to accept the quote proposed for an order.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order whose quote is accepted.
/// </param>
public record AcceptQuoteCommand(int OrderId);
