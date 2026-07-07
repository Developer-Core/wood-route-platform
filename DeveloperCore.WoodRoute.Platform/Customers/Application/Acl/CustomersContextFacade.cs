using DeveloperCore.WoodRoute.Platform.Customers.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Customers.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Customers.Interfaces.Acl;

namespace DeveloperCore.WoodRoute.Platform.Customers.Application.Acl;

/// <summary>
///     Anti-corruption facade over the Customers bounded context.
/// </summary>
/// <param name="customerQueryService">
///     Customer query service
/// </param>
/// <param name="customerCommandService">
///     Customer command service
/// </param>
public class CustomersContextFacade(
    ICustomerQueryService customerQueryService,
    ICustomerCommandService customerCommandService) : ICustomersContextFacade
{
    /// <inheritdoc />
    public async Task<int> FindOrCreateCustomerForUserAsync(int userId, string firstName, string lastName,
        string email, CancellationToken cancellationToken = default)
    {
        var existing = await customerQueryService.Handle(new GetCustomerByUserIdQuery(userId), cancellationToken);
        if (existing is not null)
            return existing.Id;

        var result = await customerCommandService.Handle(
            new CreateCustomerCommand(firstName, lastName, email, string.Empty, userId), cancellationToken);

        if (result.IsFailure)
            throw new InvalidOperationException(
                $"Could not create a customer for user {userId}: {result.Error.Message}");

        return result.Value.Id;
    }

    /// <inheritdoc />
    public async Task<int?> FindCustomerIdByUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        var customer = await customerQueryService.Handle(new GetCustomerByUserIdQuery(userId), cancellationToken);
        return customer?.Id;
    }
}
