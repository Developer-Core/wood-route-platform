namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;

/// <summary>
///     Customer resource for the REST API.
/// </summary>
/// <param name="Id">
///     The identifier of the customer.
/// </param>
/// <param name="FirstName">
///     The first name of the customer.
/// </param>
/// <param name="LastName">
///     The last name of the customer.
/// </param>
/// <param name="FullName">
///     The full name of the customer.
/// </param>
/// <param name="Email">
///     The email address of the customer.
/// </param>
/// <param name="Phone">
///     The phone (WhatsApp) number of the customer.
/// </param>
/// <param name="UserId">
///     The identifier of the account the customer is linked to, when any.
/// </param>
public record CustomerResource(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Phone,
    int? UserId);
