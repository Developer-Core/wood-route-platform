using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Queries;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Application.QueryServices;

/// <summary>
///     Production query service contract.
/// </summary>
public interface IProductionQueryService
{
    /// <summary>
    ///     Handles the get stages by order id query.
    /// </summary>
    Task<IEnumerable<Stage>> Handle(GetStagesByOrderIdQuery query, CancellationToken cancellationToken = default);
}
