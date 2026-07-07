namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;

/// <summary>
///     Query to get a customer by its email address.
/// </summary>
/// <param name="Email">
///     The email address of the customer to retrieve.
/// </param>
public record GetCustomerByEmailQuery(string Email);
