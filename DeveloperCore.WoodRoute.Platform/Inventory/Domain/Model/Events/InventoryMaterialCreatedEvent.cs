using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Events;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Events;

/// <summary>
///     Domain event raised when a new inventory material is created.
/// </summary>
public record InventoryMaterialCreatedEvent(int MaterialId) : IEvent;