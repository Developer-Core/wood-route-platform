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
    /// <param name="command">
    ///     The <see cref="CreateProfileCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> wrapping the created <see cref="Profile" />.
    /// </returns>
    Task<Result<Profile>> Handle(CreateProfileCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update profile command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateProfileCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> wrapping the updated <see cref="Profile" />.
    /// </returns>
    Task<Result<Profile>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken = default);
}
