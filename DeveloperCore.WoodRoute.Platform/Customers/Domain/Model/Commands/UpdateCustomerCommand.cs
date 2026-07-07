namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;

/// <summary>
///     Command to update the contact information of an existing customer.
/// </summary>
/// <param name="CustomerId">
///     The identifier of the customer to update.
/// </param>
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
public record UpdateCustomerCommand(
    int CustomerId,
    string FirstName,
    string LastName,
    string Email,
    string Phone);
