using DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

/// <summary>
///     Response DTO for the public order tracking endpoint.
///     Contains only the information relevant for a client to track their order.
/// </summary>
/// <param name="PublicTrackingId">The public GUID used to access this tracking page.</param>
/// <param name="CurrentStage">Current stage of production in the workshop.</param>
/// <param name="EstimatedDeliveryDate">Estimated date the order will be ready, if set.</param>
/// <param name="ProgressPercent">Overall completion percentage (0-100).</param>
public record TrackingStatusResource(
    Guid PublicTrackingId,
    EProductionStage CurrentStage,
    DateTimeOffset? EstimatedDeliveryDate,
    int ProgressPercent
);
