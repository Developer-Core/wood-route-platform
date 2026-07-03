using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest;

/// <summary>
///     REST controller for order messaging.
///     Exposes endpoints for sending messages and consulting conversation history.
/// </summary>
[ApiController]
[Route("api/v1/orders/{orderId:int}/messages")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Message Endpoints.")]
public class MessagesController(
    IMessageCommandService messageCommandService,
    IMessageQueryService messageQueryService)
    : ControllerBase
{
    /// <summary>
    ///     Sends a new message in the conversation of the specified order.
    ///     Automatically creates a conversation if none exists yet.
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        "Send Message",
        "Send a new message in the conversation linked to the specified order.",
        OperationId = "SendMessage")]
    [SwaggerResponse(201, "The message was sent.", typeof(MessageResource))]
    [SwaggerResponse(400, "The message content is empty or invalid.")]
    [SwaggerResponse(404, "The order was not found.")]
    public async Task<IActionResult> SendMessage(int orderId, [FromBody] SendMessageResource resource,
        CancellationToken cancellationToken)
    {
        var command = MessageResourceFromEntityAssembler.ToCommandFromResource(orderId, resource);
        var result = await messageCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error.Code == EngagementErrors.MessageContentEmpty.Code
                ? StatusCodes.Status400BadRequest
                : StatusCodes.Status404NotFound;

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Message,
                instance: HttpContext.Request.Path);
        }

        var messageResource = MessageResourceFromEntityAssembler.ToResourceFromEntity(result.Value);
        return CreatedAtAction(nameof(SendMessage), new { orderId }, messageResource);
    }

    /// <summary>
    ///     Returns the message history for the specified order conversation in descending
    ///     chronological order (newest first). Supports cursor-based pagination via the
    ///     <paramref name="before" /> parameter.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        "Get Message History",
        "Get the message history of an order conversation, newest messages first.",
        OperationId = "GetMessageHistory")]
    [SwaggerResponse(200, "The message history was retrieved.", typeof(IEnumerable<MessageResource>))]
    public async Task<IActionResult> GetMessageHistory(
        int orderId,
        [FromQuery] int limit = 20,
        [FromQuery] DateTimeOffset? before = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMessagesQuery(orderId, limit, before);
        var messages = await messageQueryService.Handle(query, cancellationToken);
        var resources = messages.Select(MessageResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
