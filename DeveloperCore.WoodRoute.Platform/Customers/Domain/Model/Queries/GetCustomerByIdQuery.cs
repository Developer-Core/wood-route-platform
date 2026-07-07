namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;

/// <summary>
///     Query to get a customer by its id.
/// </summary>
/// <param name="CustomerId">
///     The identifier of the customer to retrieve.
/// </param>
public record GetCustomerByIdQuery(int CustomerId);
