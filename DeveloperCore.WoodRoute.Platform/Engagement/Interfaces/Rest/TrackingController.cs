using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
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
public class TrackingController(
    IMessageQueryService messageQueryService,
    IProductionQueryService productionQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    /// <summary>
    ///     Returns the current production status of an order using its public tracking id:
    ///     the workshop's real production stages, the completion percentage and an estimated
    ///     delivery date derived from the remaining stages' estimated days.
    ///     Returns 404 if the tracking id does not exist.
    /// </summary>
    [HttpGet("{publicTrackingId:guid}")]
    [AllowAnonymous]
    [SwaggerOperation(
        "Get Order Tracking Status",
        "Returns the real production stages and progress of an order. No authentication required.",
        OperationId = "GetOrderTrackingStatus")]
    [SwaggerResponse(200, "The order tracking status was found.", typeof(TrackingStatusResource))]
    [SwaggerResponse(404, "No order was found with the specified tracking id.")]
    public async Task<IActionResult> GetOrderTrackingStatus(Guid publicTrackingId,
        CancellationToken cancellationToken)
    {
        var conversation = await messageQueryService.Handle(
            new GetConversationByTrackingIdQuery(publicTrackingId), cancellationToken);

        if (conversation is null)
            return problemDetailsFactory.CreateFromError(this,
                EngagementActionResultAssembler.ToStatusCode(EngagementErrors.TrackingIdNotFound),
                EngagementErrors.TrackingIdNotFound);

        var stages = (await productionQueryService.Handle(
                new GetStagesByOrderIdQuery(conversation.OrderId), cancellationToken))
            .OrderBy(stage => stage.OrderIndex)
            .ToList();

        var stageResources = stages
            .Select(stage => new TrackingStageResource(
                stage.Name, stage.Status.ToString(), stage.EstimatedTimeInDays, stage.OrderIndex))
            .ToList();

        var progressPercent = stages.Count == 0
            ? 0
            : (int)Math.Round(100.0 * stages.Count(stage => stage.Status == EStageStatus.Completed) / stages.Count);

        // Estimated delivery = today plus the estimated days of the stages still pending.
        var remainingDays = stages
            .Where(stage => stage.Status != EStageStatus.Completed)
            .Sum(stage => stage.EstimatedTimeInDays);
        DateTimeOffset? estimatedDeliveryDate = stages.Count == 0
            ? null
            : DateTimeOffset.UtcNow.AddDays(remainingDays);

        var resource = new TrackingStatusResource(
            conversation.PublicTrackingId, progressPercent, estimatedDeliveryDate, stageResources);

        return Ok(resource);
    }
}
