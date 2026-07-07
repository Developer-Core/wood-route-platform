namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A single stage definition sent by the carpenter when planning the production.
/// </summary>
/// <param name="Name">Name of the stage, e.g. "Corte", "Acabado".</param>
/// <param name="EstimatedTimeInDays">How many working days this stage is expected to take.</param>
public record StageDefinitionResource(string Name, int EstimatedTimeInDays);

/// <summary>
///     Request body for POST /orders/{orderId}/stages.
///     The carpenter sends this to define the production plan for an accepted order.
/// </summary>
/// <param name="Stages">Ordered list of stages to create.</param>
public record DefineStagesResource(IReadOnlyList<StageDefinitionResource> Stages);
