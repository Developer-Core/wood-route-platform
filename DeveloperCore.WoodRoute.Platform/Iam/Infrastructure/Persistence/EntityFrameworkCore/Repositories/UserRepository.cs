using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     User repository implementation.
/// </summary>
/// <param name="context">The database context.</param>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    /// <inheritdoc />
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>().AnyAsync(user => user.Email == email, cancellationToken);
    }
}
