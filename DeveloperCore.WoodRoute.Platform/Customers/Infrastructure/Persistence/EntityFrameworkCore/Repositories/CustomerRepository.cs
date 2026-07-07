using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Customers.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Customers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Customer repository implementation.
/// </summary>
/// <param name="context">The database context</param>
public class CustomerRepository(AppDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
{
    /// <inheritdoc />
    public async Task<Customer?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Customer>()
            .FirstOrDefaultAsync(customer => customer.Email.Address == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Customer?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Customer>()
            .FirstOrDefaultAsync(customer => customer.UserId == userId, cancellationToken);
    }
}
