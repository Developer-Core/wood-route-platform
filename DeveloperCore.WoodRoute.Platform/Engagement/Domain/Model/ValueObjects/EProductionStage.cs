namespace DeveloperCore.WoodRoute.Platform.Engagement.Domain.Model.ValueObjects;

/// <summary>
///     Production stages a carpentry order goes through in the workshop.
///     The order of these values matches the natural workflow progression.
/// </summary>
public enum EProductionStage
{
    NotStarted,
    MaterialPreparation,
    Cutting,
    Assembly,
    Finishing,
    QualityCheck,
    Completed
}
