using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest;

/// <summary>
///     REST controller for production planning and stage management.
///     Exposes endpoints for defining stages and updating their status.
/// </summary>
[ApiController]
[Route("api/v1/orders/{orderId:int}/stages")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Production Stage Endpoints.")]
public class ProductionController(
    IProductionCommandService productionCommandService,
    IProductionQueryService productionQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Defines the production stages for an accepted order.
    ///     Returns 409 if the order is not accepted or if stages were already defined.
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
        var command = StageResourceAssembler.ToCommandFromResource(orderId, resource);
        var result = await productionCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error.Code switch
            {
                var c when c == ManufacturingErrors.ManufactureOrderNotFound.Code => StatusCodes.Status404NotFound,
                var c when c == ManufacturingErrors.OrderNotAccepted.Code => StatusCodes.Status409Conflict,
                var c when c == ManufacturingErrors.StagesAlreadyDefined.Code => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Message,
                instance: HttpContext.Request.Path);
        }

        var resources = StageResourceAssembler.ToResourceListFromOrder(result.Value);
        return CreatedAtAction(nameof(DefineProductionStages), new { orderId }, resources);
    }

    /// <summary>
    ///     Updates the status of a specific production stage.
    ///     Returns 403 if the requesting user is not the assigned carpenter.
    ///     Publishes <c>StageUpdatedDomainEvent</c> on success.
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
        // Parse the status string coming from the request body
        if (!Enum.TryParse<EStageStatus>(resource.Status, ignoreCase: true, out var parsedStatus))
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Manufacturing.InvalidStatusValue",
                detail: $"'{resource.Status}' is not a valid stage status. Allowed values: Pending, InProgress, Completed.",
                instance: HttpContext.Request.Path);

        var command = new UpdateStageStatusCommand(orderId, stageId, parsedStatus, resource.RequestingUserId);
        var result = await productionCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error.Code switch
            {
                var c when c == ManufacturingErrors.ManufactureOrderNotFound.Code => StatusCodes.Status404NotFound,
                var c when c == ManufacturingErrors.StageNotFound.Code => StatusCodes.Status404NotFound,
                var c when c == ManufacturingErrors.UnauthorizedStageUpdate.Code => StatusCodes.Status403Forbidden,
                var c when c == ManufacturingErrors.InvalidStageTransition.Code => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Message,
                instance: HttpContext.Request.Path);
        }

        return Ok(StageResourceAssembler.ToResourceFromEntity(result.Value));
    }
}
