using DeveloperCore.WoodRoute.Platform.Profiles.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Application.Internal.CommandServices;

/// <summary>
///     Profile command service implementation.
/// </summary>
/// <param name="profileRepository">
///     Profile repository.
/// </param>
/// <param name="unitOfWork">
///     Unit of work.
/// </param>
public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Result<Profile>> Handle(CreateProfileCommand command,
        CancellationToken cancellationToken = default)
    {
        var existingProfile = await profileRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (existingProfile is not null) return Result<Profile>.Failure(ProfileErrors.EmailAlreadyRegistered);

        var profile = new Profile(command);
        await profileRepository.AddAsync(profile, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Profile>.Success(profile);
    }

    /// <inheritdoc />
    public async Task<Result<Profile>> Handle(UpdateProfileCommand command,
        CancellationToken cancellationToken = default)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId, cancellationToken);
        if (profile is null) return Result<Profile>.Failure(ProfileErrors.ProfileNotFound);

        var error = profile.Update(command);
        if (error != Error.None) return Result<Profile>.Failure(error);

        profileRepository.Update(profile);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Profile>.Success(profile);
    }
}
