using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Iam.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Iam.Interfaces.Rest;

/// <summary>
///     REST controller for authentication, covering user registration (TS09) and login (TS01).
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication Endpoints.")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    /// <summary>
    ///     Registers a new user and returns the authenticated user together with a JWT token (TS09).
    /// </summary>
    /// <param name="resource">The <see cref="SignUpResource" /> with the registration data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The authenticated user resource including the JWT token.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation("Register", "Register a new user and issue a JWT token.", OperationId = "Register")]
    [SwaggerResponse(201, "The user was registered.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(409, "The email is already registered.")]
    public async Task<IActionResult> Register(SignUpResource resource, CancellationToken cancellationToken)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            authenticated => StatusCode(StatusCodes.Status201Created,
                AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticated.user,
                    authenticated.token)));
    }

    /// <summary>
    ///     Authenticates a user and returns the authenticated user together with a JWT token (TS01).
    /// </summary>
    /// <param name="resource">The <see cref="SignInResource" /> with the credentials to validate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The authenticated user resource including the JWT token.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation("Login", "Authenticate a user and issue a JWT token.", OperationId = "Login")]
    [SwaggerResponse(200, "The user was authenticated.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(401, "Invalid email or password.")]
    public async Task<IActionResult> Login(SignInResource resource, CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            authenticated => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticated.user,
                authenticated.token)));
    }
}
