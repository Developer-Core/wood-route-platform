namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.Commands;

/// <summary>
///     Command to accept a pending order, claiming it from the pool when it is still unassigned.
/// </summary>
/// <param name="OrderId">
///     The identifier of the order to accept.
/// </param>
/// <param name="CarpenterId">
///     The identifier of the carpenter accepting the order. When the order is still unassigned it is
///     assigned to this carpenter before being accepted.
/// </param>
public record AcceptOrderCommand(int OrderId, int CarpenterId);
