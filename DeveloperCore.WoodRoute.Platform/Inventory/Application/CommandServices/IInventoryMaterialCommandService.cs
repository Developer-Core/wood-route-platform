using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Aggregates;
using DeveloperCore.WoodRoute.Platform.Inventory.Domain.Model.Commands;
using DeveloperCore.WoodRoute.Platform.Shared.Application.Model;

namespace DeveloperCore.WoodRoute.Platform.Inventory.Application.CommandServices;

/// <summary>
///     Inventory material command service contract.
/// </summary>
public interface IInventoryMaterialCommandService
{
    /// <summary>
    ///     Handles the create inventory material command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateInventoryMaterialCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{InventoryMaterial}" /> with the created inventory material.
    /// </returns>
    Task<Result<InventoryMaterial>> Handle(CreateInventoryMaterialCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the update inventory material command.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateInventoryMaterialCommand" /> to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{InventoryMaterial}" /> with the updated inventory material.
    /// </returns>
    Task<Result<InventoryMaterial>> Handle(UpdateInventoryMaterialCommand command,
        CancellationToken cancellationToken = default);
}
