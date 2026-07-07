using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest;

/// <summary>
///     REST controller for production planning and stage management.
/// </summary>
/// <remarks>
///     Production planning is carpenter-only: every endpoint requires the
///     <see cref="EUserRole.Carpenter" /> role.
/// </remarks>
[ApiController]
[Route("api/v1/orders/{orderId:int}/stages")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(EUserRole.Carpenter)]
[SwaggerTag("Available Production Stage Endpoints.")]
public class ProductionController(
    IProductionCommandService productionCommandService,
    IProductionQueryService productionQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>
    ///     Defines the production stages for an accepted order.
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        "Define Production Stages",
        "Define the ordered production stages for an accepted order.",
        OperationId = "DefineProductionStages")]
    [SwaggerResponse(201, "The stages were created.", typeof(IEnumerable<StageResource>))]
    [SwaggerResponse(400, "The stage list is empty or invalid.")]
    [SwaggerResponse(404, "The order was not found.")]
    [SwaggerResponse(409, "The order is not accepted, or stages were already defined.")]
    public async Task<IActionResult> DefineProductionStages(int orderId, [FromBody] DefineStagesResource resource,
        CancellationToken cancellationToken)
    {
        // The acting carpenter is derived from the JWT-backed token (never from client input) so the
        // ownership check in the command service cannot be spoofed via the request body.
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        var command = StageResourceAssembler.ToCommandFromResource(orderId, user.Id, resource);
        var result = await productionCommandService.Handle(command, cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            order => CreatedAtAction(nameof(GetStages), new { orderId },
                StageResourceAssembler.ToResourceListFromOrder(order)));
    }

    /// <summary>
    ///     Returns the production stages of an order for the client progress view.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        "Get Production Stages",
        "Get the ordered production stages of an order.",
        OperationId = "GetProductionStages")]
    [SwaggerResponse(200, "The stages were found and returned.", typeof(IEnumerable<StageResource>))]
    public async Task<IActionResult> GetStages(int orderId, CancellationToken cancellationToken)
    {
        var stages = await productionQueryService.Handle(new GetStagesByOrderIdQuery(orderId), cancellationToken);
        return Ok(StageResourceAssembler.ToResourceListFromStages(stages));
    }

    /// <summary>
    ///     Updates the status of a specific production stage.
    /// </summary>
    [HttpPatch("{stageId:int}")]
    [SwaggerOperation(
        "Update Stage Status",
        "Update the status of a production stage. Only the assigned carpenter can do this.",
        OperationId = "UpdateStageStatus")]
    [SwaggerResponse(200, "The stage status was updated.", typeof(StageResource))]
    [SwaggerResponse(400, "The status value is invalid or the transition is not allowed.")]
    [SwaggerResponse(403, "The requesting user is not the carpenter assigned to this order.")]
    [SwaggerResponse(404, "The order or stage was not found.")]
    public async Task<IActionResult> UpdateStageStatus(int orderId, int stageId,
        [FromBody] UpdateStageStatusResource resource,
        CancellationToken cancellationToken)
    {
        // The acting carpenter is derived from the JWT-backed token (never from client input) so the
        // ownership check in the command service cannot be spoofed via the request body.
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        if (!Enum.TryParse<EStageStatus>(resource.Status, ignoreCase: true, out var parsedStatus))
        {
            var invalidStatusError = new Error("Manufacturing.InvalidStatusValue",
                $"'{resource.Status}' is not a valid stage status. Allowed values: Pending, InProgress, Completed.");
            return problemDetailsFactory.CreateFromError(this,
                ManufacturingActionResultAssembler.ToStatusCode(invalidStatusError), invalidStatusError,
                resource.Status);
        }

        var command = new UpdateStageStatusCommand(orderId, stageId, parsedStatus, user.Id);
        var result = await productionCommandService.Handle(command, cancellationToken);

        return ManufacturingActionResultAssembler.ToActionResultFromResult(this, problemDetailsFactory, result,
            stage => Ok(StageResourceAssembler.ToResourceFromEntity(stage)));
    }
}
