using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Repositories;

/// <summary>
///     Profile repository interface.
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    /// <summary>
    ///     Find a profile by its email address.
    /// </summary>
    Task<Profile?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
}
