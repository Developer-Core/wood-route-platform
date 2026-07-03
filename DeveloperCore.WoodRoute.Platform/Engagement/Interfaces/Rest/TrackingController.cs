using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest;

/// <summary>
///     Public REST controller for order tracking.
///     No authentication is required — the client uses the public tracking id
///     received when placing their order.
/// </summary>
[ApiController]
[Route("api/v1/tracking")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Tracking Endpoints.")]
public class TrackingController(IMessageQueryService messageQueryService) : ControllerBase
{
    /// <summary>
    ///     Returns the current production status of an order using its public tracking id.
    ///     Returns 404 if the tracking id does not exist.
    /// </summary>
    [HttpGet("{publicTrackingId:guid}")]
    [SwaggerOperation(
        "Get Order Tracking Status",
        "Returns the current production stage and progress of an order. No authentication required.",
        OperationId = "GetOrderTrackingStatus")]
    [SwaggerResponse(200, "The order tracking status was found.", typeof(TrackingStatusResource))]
    [SwaggerResponse(404, "No order was found with the specified tracking id.")]
    public async Task<IActionResult> GetOrderTrackingStatus(Guid publicTrackingId,
        CancellationToken cancellationToken)
    {
        var query = new GetConversationByTrackingIdQuery(publicTrackingId);
        var conversation = await messageQueryService.Handle(query, cancellationToken);

        if (conversation is null)
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: EngagementErrors.TrackingIdNotFound.Code,
                detail: EngagementErrors.TrackingIdNotFound.Message,
                instance: HttpContext.Request.Path);

        var resource = new TrackingStatusResource(
            conversation.PublicTrackingId,
            conversation.CurrentStage,
            conversation.EstimatedDeliveryDate,
            conversation.ProgressPercent);

        return Ok(resource);
    }
}
