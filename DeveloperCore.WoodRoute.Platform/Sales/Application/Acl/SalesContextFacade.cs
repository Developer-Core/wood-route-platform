using DeveloperCore.WoodRoute.Platform.Sales.Application.QueryServices;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Queries;
using DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;
using DeveloperCore.WoodRoute.Platform.Sales.Interfaces.Acl;

namespace DeveloperCore.WoodRoute.Platform.Sales.Application.Acl;

/// <summary>
///     Anti-corruption facade over the Sales bounded context.
/// </summary>
/// <param name="orderQueryService">
///     Order query service
/// </param>
public class SalesContextFacade(IOrderQueryService orderQueryService) : ISalesContextFacade
{
    /// <inheritdoc />
    public async Task<bool> IsOrderAcceptedAsync(int orderId)
    {
        var order = await orderQueryService.Handle(new GetOrderByIdQuery(orderId));
        return order is { Status: EOrderStatus.Accepted };
    }

    /// <inheritdoc />
    public async Task<int?> GetOrderIdByPublicTrackingIdAsync(Guid publicTrackingId)
    {
        var order = await orderQueryService.Handle(new GetOrderByPublicTrackingIdQuery(publicTrackingId));
        return order?.Id;
    }

    /// <inheritdoc />
    public async Task<Guid?> GetPublicTrackingIdByOrderIdAsync(int orderId)
    {
        var order = await orderQueryService.Handle(new GetOrderByIdQuery(orderId));
        return order?.PublicTrackingId;
    }
}
