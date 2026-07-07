namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;

/// <summary>
///     Command to create a new customer.
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
/// <param name="UserId">
///     The identifier of the account the customer is linked to, when any.
/// </param>
public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    int? UserId);
