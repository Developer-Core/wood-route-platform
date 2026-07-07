namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;

/// <summary>
///     Query to get a customer by the account it is linked to.
/// </summary>
/// <param name="UserId">
///     The identifier of the linked account.
/// </param>
public record GetCustomerByUserIdQuery(int UserId);
