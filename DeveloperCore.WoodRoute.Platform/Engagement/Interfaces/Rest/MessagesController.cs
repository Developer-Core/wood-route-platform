using System.Net.Mime;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;
using DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Transform;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;
using DeveloperCore.WoodRoute.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest;

/// <summary>
///     REST controller for order messaging.
///     Exposes endpoints for sending messages and consulting conversation history.
/// </summary>
/// <remarks>
///     Messaging is available to any authenticated user: both carpenters and clients take part in
///     the conversation of an order.
/// </remarks>
[ApiController]
[Route("api/v1/orders/{orderId:int}/messages")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
[SwaggerTag("Available Message Endpoints.")]
public class MessagesController(
    IMessageCommandService messageCommandService,
    IMessageQueryService messageQueryService,
    ISalesContextFacade salesContextFacade,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>
    ///     Ensures the authenticated user takes part in the order (as its customer or carpenter)
    ///     before reading or writing its conversation. Ownership is resolved from Sales through the
    ///     anti-corruption facade, never from client input, to prevent object-level authorization
    ///     bypass (IDOR). Returns a problem-details result to short-circuit the action when access is
    ///     denied (rendered as a 404 not-found so the existence of other orders is never leaked), or
    ///     <c>null</c> when access is granted.
    /// </summary>
    private async Task<IActionResult?> EnsureOrderParticipationAsync(int orderId)
    {
        var user = HttpContext.GetAuthenticatedUser();
        if (user is null)
            return Unauthorized();

        if (!await salesContextFacade.IsOrderParticipantAsync(orderId, user.Id))
            return problemDetailsFactory.CreateFromError(this,
                EngagementActionResultAssembler.ToStatusCode(EngagementErrors.ConversationNotFound),
                EngagementErrors.ConversationNotFound);

        return null;
    }

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
        var accessDenied = await EnsureOrderParticipationAsync(orderId);
        if (accessDenied is not null)
            return accessDenied;

        var command = MessageResourceFromEntityAssembler.ToCommandFromResource(orderId, resource);
        var result = await messageCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return problemDetailsFactory.CreateFromError(this,
                EngagementActionResultAssembler.ToStatusCode(result.Error), result.Error);

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
        var accessDenied = await EnsureOrderParticipationAsync(orderId);
        if (accessDenied is not null)
            return accessDenied;

        var query = new GetMessagesQuery(orderId, limit, before);
        var messages = await messageQueryService.Handle(query, cancellationToken);
        var resources = messages.Select(MessageResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
