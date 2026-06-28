using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.ValueObjects;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Response DTO for a single production stage.
/// </summary>
/// <param name="Id">Unique id of the stage.</param>
/// <param name="Name">Name of the stage.</param>
/// <param name="EstimatedTimeInDays">Planned duration in working days.</param>
/// <param name="OrderIndex">Position in the production sequence (0-based).</param>
/// <param name="Status">Current status: Pending, InProgress or Completed.</param>
/// <param name="StartedAt">When the carpenter started this stage, if applicable.</param>
/// <param name="CompletedAt">When the carpenter completed this stage, if applicable.</param>
public record StageResource(
    int Id,
    string Name,
    int EstimatedTimeInDays,
    int OrderIndex,
    EStageStatus Status,
    DateTimeOffset? StartedAt,
    DateTimeOffset? CompletedAt
);
