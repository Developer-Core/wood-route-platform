namespace DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the contact information of a customer.
/// </summary>
/// <param name="FirstName">
///     The first name of the customer.
/// </param>
/// <param name="LastName">
///     The last name of the customer.
/// </param>
/// <param name="Email">
///     The email address of the customer.
/// </param>
/// <param name="Phone">
///     The phone (WhatsApp) number of the customer.
/// </param>
public record UpdateCustomerResource(
    string FirstName,
    string LastName,
    string Email,
    string Phone);
