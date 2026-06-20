using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.Internal.CommandServices;
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
    IMessageCommandService messageCommandService)
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
        var command = MessageResourceAssembler.ToCommandFromResource(orderId, resource);
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

        var messageResource = MessageResourceAssembler.ToResourceFromEntity(result.Value);
        return CreatedAtAction(nameof(SendMessage), new { orderId }, messageResource);
    }
}
