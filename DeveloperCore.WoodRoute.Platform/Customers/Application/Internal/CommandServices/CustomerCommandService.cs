using DeveloperCore.WoodRoute.Platform.Customers.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Customers.Application.Internal.CommandServices;

/// <summary>
///     Customer command service implementation.
/// </summary>
/// <param name="customerRepository">
///     Customer repository.
/// </param>
/// <param name="unitOfWork">
///     Unit of work.
/// </param>
public class CustomerCommandService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    : ICustomerCommandService
{
    /// <inheritdoc />
    public async Task<Result<Customer>> Handle(CreateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var existingCustomer = await customerRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (existingCustomer is not null) return Result<Customer>.Failure(CustomerErrors.EmailAlreadyRegistered);

        var customer = new Customer(command);
        await customerRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Customer>.Success(customer);
    }

    /// <inheritdoc />
    public async Task<Result<Customer>> Handle(UpdateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.FindByIdAsync(command.CustomerId, cancellationToken);
        if (customer is null) return Result<Customer>.Failure(CustomerErrors.CustomerNotFound);

        var error = customer.Update(command);
        if (error != Error.None) return Result<Customer>.Failure(error);

        customerRepository.Update(customer);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Customer>.Success(customer);
    }
}
