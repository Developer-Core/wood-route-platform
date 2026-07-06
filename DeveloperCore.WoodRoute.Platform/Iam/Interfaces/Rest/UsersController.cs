using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Iam.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest;

/// <summary>
///     REST controller for user directory queries (e.g. listing carpenters a
///     customer can assign an order to).
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Endpoints.")]
public class UsersController(IUserQueryService userQueryService) : ControllerBase
{
    /// <summary>
    ///     Gets all users, optionally filtered by role (e.g. <c>?role=Carpenter</c>).
    /// </summary>
    [HttpGet]
    [SwaggerOperation("Get All Users", "Get all users, optionally filtered by role.", OperationId = "GetAllUsers")]
    [SwaggerResponse(200, "The users were found and returned.", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers([FromQuery] string? role, CancellationToken cancellationToken)
    {
        var users = await userQueryService.Handle(new GetAllUsersQuery(), cancellationToken);

        if (!string.IsNullOrWhiteSpace(role))
            users = users.Where(user => string.Equals(user.Role.ToString(), role, StringComparison.OrdinalIgnoreCase));

        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
