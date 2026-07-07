using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to map between Manufacturing domain objects and REST resources.
/// </summary>
public static class StageResourceAssembler
{
    /// <summary>Builds a <see cref="DefineStagesCommand" /> from the REST request resource.</summary>
    /// <remarks>
    ///     The carpenter id is taken from the authenticated identity (JWT-backed token) supplied by the
    ///     controller, never from client input, to prevent authorization spoofing.
    /// </remarks>
    public static DefineStagesCommand ToCommandFromResource(int salesOrderId, int carpenterId,
        DefineStagesResource resource)
    {
        var stages = resource.Stages
            .Select(s => new StageDefinition(s.Name, s.EstimatedTimeInDays))
            .ToList()
            .AsReadOnly();

        return new DefineStagesCommand(salesOrderId, carpenterId, stages);
    }

    /// <summary>Builds an <see cref="UpdateStagesCommand" /> from the REST request resource.</summary>
    /// <remarks>
    ///     The carpenter id is taken from the authenticated identity (JWT-backed token) supplied by the
    ///     controller, never from client input, to prevent authorization spoofing.
    /// </remarks>
    public static UpdateStagesCommand ToUpdateCommandFromResource(int salesOrderId, int carpenterId,
        DefineStagesResource resource)
    {
        var stages = resource.Stages
            .Select(s => new StageDefinition(s.Name, s.EstimatedTimeInDays))
            .ToList()
            .AsReadOnly();

        return new UpdateStagesCommand(salesOrderId, carpenterId, stages);
    }

    /// <summary>Builds a <see cref="StageResource" /> from a domain <see cref="Stage" /> entity.</summary>
    public static StageResource ToResourceFromEntity(Stage stage)
    {
        return new StageResource(
            stage.Id,
            stage.Name,
            stage.EstimatedTimeInDays,
            stage.OrderIndex,
            stage.Status,
            stage.StartedAt,
            stage.CompletedAt);
    }

    /// <summary>Projects the stages of a <see cref="ManufactureOrder" /> to resources, ordered by sequence.</summary>
    public static IEnumerable<StageResource> ToResourceListFromOrder(ManufactureOrder order)
    {
        return ToResourceListFromStages(order.Stages);
    }

    /// <summary>Projects a collection of <see cref="Stage" /> entities to resources, ordered by sequence.</summary>
    public static IEnumerable<StageResource> ToResourceListFromStages(IEnumerable<Stage> stages)
    {
        return stages
            .OrderBy(s => s.OrderIndex)
            .Select(ToResourceFromEntity);
    }
}
