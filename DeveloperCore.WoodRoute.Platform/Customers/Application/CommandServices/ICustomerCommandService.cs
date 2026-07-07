using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Customers.Application.CommandServices;

/// <summary>
///     Customer command service contract.
/// </summary>
public interface ICustomerCommandService
{
    /// <summary>
    ///     Handles the create customer command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateCustomerCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> wrapping the created <see cref="Customer" />.
    /// </returns>
    Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update customer command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateCustomerCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> wrapping the updated <see cref="Customer" />.
    /// </returns>
    Task<Result<Customer>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken = default);
}
