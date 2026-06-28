namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request body for PATCH /orders/{orderId}/stages/{stageId}.
///     The carpenter sends this when they start or finish a production stage.
/// </summary>
/// <param name="Status">
///     New status to apply: "Pending", "InProgress" or "Completed".
///     Only forward transitions are accepted.
/// </param>
/// <param name="RequestingUserId">
///     Id of the authenticated user making the request.
///     Must match the carpenter assigned to the order, otherwise 403 is returned.
/// </param>
public record UpdateStageStatusResource(string Status, int RequestingUserId);
