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
    /// <param name="query">
    ///     The <see cref="GetAllProfilesQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A collection of all <see cref="Profile" /> instances.
    /// </returns>
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the get profile by id query.
    /// </summary>
    /// <param name="query">
    ///     The <see cref="GetProfileByIdQuery" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The matching <see cref="Profile" /> if found; otherwise <c>null</c>.
    /// </returns>
    Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken = default);
}
