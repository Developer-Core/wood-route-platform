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
    ///     Registers a new carpenter through the closed, invitation-gated flow and returns the
    ///     authenticated carpenter together with a JWT token.
    /// </summary>
    /// <param name="resource">The <see cref="SignUpCarpenterResource" /> with the registration data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The authenticated carpenter resource including the JWT token.</returns>
    [HttpPost("sign-up-carpenter")]
    [AllowAnonymous]
    [SwaggerOperation("RegisterCarpenter",
        "Register a new carpenter using a valid invitation code and issue a JWT token.",
        OperationId = "RegisterCarpenter")]
    [SwaggerResponse(201, "The carpenter was registered.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(403, "The invitation code is invalid.")]
    [SwaggerResponse(409, "The email is already registered.")]
    public async Task<IActionResult> RegisterCarpenter(SignUpCarpenterResource resource,
        CancellationToken cancellationToken)
    {
        var signUpCarpenterCommand = SignUpCarpenterCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(signUpCarpenterCommand, cancellationToken);

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
