namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to accept the quote proposed for an order.
/// </summary>
public record AcceptQuoteCommand(int OrderId);
