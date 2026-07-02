using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Application.CommandServices;

/// <summary>
///     Profile command service contract.
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    ///     Handles the create profile command.
    /// </summary>
    Task<Result<Profile>> Handle(CreateProfileCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update profile command.
    /// </summary>
    Task<Result<Profile>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken = default);
}
