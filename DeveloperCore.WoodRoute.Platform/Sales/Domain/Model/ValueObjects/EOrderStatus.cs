namespace DeveloperCore.WoodRoute.Platform.Sales.Domain.Model.ValueObjects;

/// <summary>
///     Represents the lifecycle status of a custom furniture order.
/// </summary>
public enum EOrderStatus
{
    Pending,
    Accepted,
    InProgress,
    ReadyForDelivery,
    Completed,
    Cancelled,
    Rejected
}
