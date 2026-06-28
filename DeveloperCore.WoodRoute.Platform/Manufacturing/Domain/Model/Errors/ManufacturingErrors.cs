using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Domain errors for the Manufacturing bounded context.
/// </summary>
public static class ManufacturingErrors
{
    public static readonly Error ManufactureOrderNotFound =
        new("Manufacturing.ManufactureOrderNotFound", "No manufacture order was found for the specified sales order.");

    public static readonly Error StagesAlreadyDefined =
        new("Manufacturing.StagesAlreadyDefined",
            "The production stages for this order have already been defined and cannot be changed.");

    public static readonly Error StageNotFound =
        new("Manufacturing.StageNotFound", "The specified stage was not found in this manufacture order.");

    public static readonly Error InvalidStageTransition =
        new("Manufacturing.InvalidStageTransition",
            "The stage status can only move forward: Pending -> InProgress -> Completed.");

    public static readonly Error OrderNotAccepted =
        new("Manufacturing.OrderNotAccepted",
            "Production stages can only be defined for orders that have been accepted by the carpenter.");

    public static readonly Error UnauthorizedStageUpdate =
        new("Manufacturing.UnauthorizedStageUpdate",
            "Only the carpenter assigned to this order can update its production stages.");

    public static readonly Error EmptyStageList =
        new("Manufacturing.EmptyStageList", "At least one production stage must be defined.");
}
