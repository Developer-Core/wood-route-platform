namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;

/// <summary>
///     A single stage definition within a <see cref="DefineStagesCommand" />.
/// </summary>
public record StageDefinition(string Name, int EstimatedTimeInDays);
