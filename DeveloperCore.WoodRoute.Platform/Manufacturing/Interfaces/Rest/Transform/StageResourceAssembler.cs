using DeveloperCore.WoodRoute.Platform.Manufacturing.Application.Internal.CommandServices.Commands;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Resources;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler to map between Manufacturing domain objects and REST resources.
/// </summary>
public static class StageResourceAssembler
{
    /// <summary>Builds a <see cref="DefineStagesCommand" /> from the REST request resource.</summary>
    public static DefineStagesCommand ToCommandFromResource(int salesOrderId, DefineStagesResource resource)
    {
        var stages = resource.Stages
            .Select(s => new StageDefinition(s.Name, s.EstimatedTimeInDays))
            .ToList()
            .AsReadOnly();

        return new DefineStagesCommand(salesOrderId, resource.CarpenterId, stages);
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

    /// <summary>
    ///     Projects all stages of a <see cref="ManufactureOrder" /> to a list of resources,
    ///     sorted by their execution order.
    /// </summary>
    public static IEnumerable<StageResource> ToResourceListFromOrder(ManufactureOrder order)
    {
        return order.Stages
            .OrderBy(s => s.OrderIndex)
            .Select(ToResourceFromEntity);
    }
}
