using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Interfaces.Rest;

/// <summary>
///     REST controller for user profile management.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile Endpoints.")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Profile", "Create a new user profile.", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile data is invalid.")]
    [SwaggerResponse(409, "The email address is already registered.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource, CancellationToken cancellationToken)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await profileCommandService.Handle(createProfileCommand, cancellationToken);

        return ProfilesActionResultAssembler.ToActionResultFromResult(this, result,
            createdProfile => CreatedAtAction(nameof(GetProfileById), new { profileId = createdProfile.Id },
                ProfileResourceFromEntityAssembler.ToResourceFromEntity(createdProfile)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Profiles", "Get all profiles.", OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "The profiles were found and returned.", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
    {
        var profiles = await profileQueryService.Handle(new GetAllProfilesQuery(), cancellationToken);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }

    [HttpGet("{profileId:int}")]
    [SwaggerOperation("Get Profile by Id", "Get a profile by its unique identifier.",
        OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId, CancellationToken cancellationToken)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery, cancellationToken);

        if (profile is null)
            return ProfilesActionResultAssembler.ToProblemFromError(this, ProfileErrors.ProfileNotFound);
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    [HttpPatch("{profileId:int}")]
    [SwaggerOperation("Update Profile", "Update the personal information of a profile.",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(200, "The profile was updated.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile data is invalid.")]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> UpdateProfile(int profileId, UpdateProfileResource resource,
        CancellationToken cancellationToken)
    {
        var updateProfileCommand = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var result = await profileCommandService.Handle(updateProfileCommand, cancellationToken);

        return ProfilesActionResultAssembler.ToActionResultFromResult(this, result,
            updatedProfile => Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(updatedProfile)));
    }
}
