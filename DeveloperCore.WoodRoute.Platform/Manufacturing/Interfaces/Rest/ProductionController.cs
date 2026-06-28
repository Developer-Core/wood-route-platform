using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;
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
}
