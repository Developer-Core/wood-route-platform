using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Events;

/// <summary>
///     Domain event raised when the stock of a material falls below the minimum stock.
/// </summary>
public record LowStockDetectedEvent(int MaterialId) : IEvent;