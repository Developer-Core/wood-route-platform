using DeveloperCore.WoodRoute.Platform.Profiles.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Application.Internal.QueryServices;

/// <summary>
///     Profile query service implementation.
/// </summary>
public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await profileRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId, cancellationToken);
    }
}
