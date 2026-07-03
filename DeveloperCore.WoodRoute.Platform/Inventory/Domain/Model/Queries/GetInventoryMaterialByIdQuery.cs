namespace DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Queries;

/// <summary>
///     Query to retrieve an inventory material by its identifier.
/// </summary>
/// <param name="MaterialId">
///     The identifier of the material to retrieve.
/// </param>
public record GetInventoryMaterialByIdQuery(int MaterialId);