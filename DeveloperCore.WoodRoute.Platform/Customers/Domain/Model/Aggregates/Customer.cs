using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;

/// <summary>
///     Customer Aggregate Root
/// </summary>
/// <remarks>
///     Represents a workshop client (CRM). A customer may be a walk-in created by the
///     carpenter without an account, or optionally linked to an existing platform account
///     through <see cref="UserId" />. Updates are applied through guarded behavior that
///     returns a domain <see cref="Error" /> (<see cref="Error.None" /> on success).
/// </remarks>
public class Customer : IAuditableEntity
{
    private Customer()
    {
        Name = null!;
        Email = null!;
        Phone = null!;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Customer" /> aggregate from a create command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateCustomerCommand" /> carrying the initial customer data.
    /// </param>
    public Customer(CreateCustomerCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Phone = ValidatePhone(command.Phone);
        UserId = command.UserId;
    }

    public int Id { get; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Phone { get; private set; }
    public int? UserId { get; private set; }

    /// <summary>
    ///     Gets the full name of the customer.
    /// </summary>
    public string FullName => Name.FullName;

    /// <summary>
    ///     Gets the email address of the customer.
    /// </summary>
    public string EmailAddress => Email.Address;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the contact information of the customer.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateCustomerCommand" /> carrying the updated customer data.
    /// </param>
    /// <returns>
    ///     A domain <see cref="Error" />; <see cref="Error.None" /> on success.
    /// </returns>
    public Error Update(UpdateCustomerCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Phone = ValidatePhone(command.Phone);
        return Error.None;
    }

    /// <summary>
    ///     Links the customer to an existing platform account.
    /// </summary>
    /// <param name="userId">
    ///     The identifier of the account to link the customer to.
    /// </param>
    public void LinkAccount(int userId)
    {
        UserId = userId;
    }

    /// <summary>
    ///     Normalizes the phone number, treating null or empty as an absent value.
    /// </summary>
    /// <remarks>
    ///     A registered client may not have a phone number, while the frontend still requires it for
    ///     walk-in customers created by a carpenter. The column stays NOT NULL, storing an empty
    ///     string when no phone is provided, so no migration is required.
    /// </remarks>
    /// <param name="phone">
    ///     The phone number to normalize.
    /// </param>
    /// <returns>
    ///     The provided phone number, or an empty string when it is null.
    /// </returns>
    private static string ValidatePhone(string phone)
    {
        return phone ?? string.Empty;
    }
}
