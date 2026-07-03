using DeveloperCore.WoodRoute.Platform.Inventory.Application.CommandServices;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Errors;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Repositories;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;
using DeveloperCore.WoodRoute.Platform.Shared.Domain.Repositories;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Application.Internal.CommandServices;

/// <summary>
///     Inventory material command service implementation.
/// </summary>
/// <param name="inventoryMaterialRepository">
///     Inventory material repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class InventoryMaterialCommandService(
    IInventoryMaterialRepository inventoryMaterialRepository,
    IUnitOfWork unitOfWork) : IInventoryMaterialCommandService
{
    /// <inheritdoc />
    public async Task<Result<InventoryMaterial>> Handle(CreateInventoryMaterialCommand command,
        CancellationToken cancellationToken = default)
    {
        var material = new InventoryMaterial(command);
        await inventoryMaterialRepository.AddAsync(material, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<InventoryMaterial>.Success(material);
    }

    /// <inheritdoc />
    public async Task<Result<InventoryMaterial>> Handle(UpdateInventoryMaterialCommand command,
        CancellationToken cancellationToken = default)
    {
        var material = await inventoryMaterialRepository.FindByIdAsync(command.MaterialId, cancellationToken);
        if (material is null) return Result<InventoryMaterial>.Failure(InventoryErrors.MaterialNotFound);

        var error = material.Update(command);
        if (error != Error.None) return Result<InventoryMaterial>.Failure(error);

        inventoryMaterialRepository.Update(material);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<InventoryMaterial>.Success(material);
    }
}
