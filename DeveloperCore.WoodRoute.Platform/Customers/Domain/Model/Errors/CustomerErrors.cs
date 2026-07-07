using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Customers bounded context.
/// </summary>
public static class CustomerErrors
{
    public static readonly Error CustomerNotFound =
        new("Customers.CustomerNotFound", "The specified customer was not found.");

    public static readonly Error EmailAlreadyRegistered =
        new("Customers.EmailAlreadyRegistered", "A customer with the specified email address already exists.");
}
