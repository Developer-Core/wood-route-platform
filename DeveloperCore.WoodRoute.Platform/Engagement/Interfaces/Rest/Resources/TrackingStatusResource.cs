namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

/// <summary>
///     Response DTO for the public order tracking endpoint. Contains only the
///     information relevant for a client to track their order: the workshop's real
///     production stages, the completion percentage and the estimated delivery date
///     (derived from the remaining stages' estimated days).
/// </summary>
/// <param name="PublicTrackingId">The public GUID used to access this tracking page.</param>
/// <param name="ProgressPercent">Overall completion percentage (0-100).</param>
/// <param name="EstimatedDeliveryDate">Estimated date the order will be ready, if any stages are defined.</param>
/// <param name="Stages">The ordered production stages of the order.</param>
public record TrackingStatusResource(
    Guid PublicTrackingId,
    int ProgressPercent,
    DateTimeOffset? EstimatedDeliveryDate,
    IEnumerable<TrackingStageResource> Stages
);
