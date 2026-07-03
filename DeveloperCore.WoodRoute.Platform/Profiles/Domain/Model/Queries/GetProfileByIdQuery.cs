namespace DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Query to get a profile by its id.
/// </summary>
/// <param name="ProfileId">
///     The identifier of the profile to retrieve.
/// </param>
public record GetProfileByIdQuery(int ProfileId);
