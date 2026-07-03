namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;

/// <summary>
///     Query to get all orders placed by a customer.
/// </summary>
/// <param name="CustomerId">
///     The identifier of the customer.
/// </param>
public record GetOrdersByCustomerIdQuery(int CustomerId);
