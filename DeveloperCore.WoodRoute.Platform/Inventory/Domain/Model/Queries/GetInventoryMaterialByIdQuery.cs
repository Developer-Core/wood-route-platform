namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Queries;

/// <summary>
///     Query to retrieve an inventory material by its identifier.
/// </summary>
public record GetInventoryMaterialByIdQuery(int MaterialId);