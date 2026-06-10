namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get all orders placed by a customer.
/// </summary>
public record GetOrdersByCustomerIdQuery(int CustomerId);
