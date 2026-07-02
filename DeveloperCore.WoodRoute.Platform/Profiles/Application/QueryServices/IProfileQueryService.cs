using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Application.QueryServices;

/// <summary>
///     Profile query service contract.
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    ///     Handles the get all profiles query.
    /// </summary>
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get profile by id query.
    /// </summary>
    Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken = default);
}
