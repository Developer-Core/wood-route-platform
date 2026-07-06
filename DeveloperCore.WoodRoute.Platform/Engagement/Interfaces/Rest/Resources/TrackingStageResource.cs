namespace DeveloperCore.WoodRoute.Platform.Engagement.Interfaces.Rest.Resources;

/// <summary>
///     A production stage of an order, exposed on the public tracking page so the
///     client sees the workshop's real stages (not a fixed generic pipeline).
/// </summary>
/// <param name="Name">Stage name (e.g. "Corte", "Acabado").</param>
/// <param name="Status">Stage status (Pending | InProgress | Completed).</param>
/// <param name="EstimatedTimeInDays">Planned duration in working days.</param>
/// <param name="OrderIndex">Position in the production sequence (0-based).</param>
public record TrackingStageResource(string Name, string Status, int EstimatedTimeInDays, int OrderIndex);
