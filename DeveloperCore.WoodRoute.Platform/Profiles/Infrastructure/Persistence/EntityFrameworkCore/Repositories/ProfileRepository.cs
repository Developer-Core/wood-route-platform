using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Profile repository implementation.
/// </summary>
public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    /// <inheritdoc />
    public async Task<Profile?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(profile => profile.Email.Address == email, cancellationToken);
    }
}
