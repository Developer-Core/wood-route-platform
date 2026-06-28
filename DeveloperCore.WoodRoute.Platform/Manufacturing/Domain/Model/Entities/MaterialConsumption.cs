using DeveloperCore.WoodRoute.Platform.Shared.Domain.Model.Entities;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Domain.Model.Entities;

/// <summary>
///     Tracks the actual consumption of a material during a production stage.
///     This helps the carpenter keep a record of what was used versus what was planned.
/// </summary>
public class MaterialConsumption : IAuditableEntity
{
    private MaterialConsumption()
    {
        MaterialName = string.Empty;
        Unit = string.Empty;
    }

    public MaterialConsumption(int stageId, string materialName, decimal quantityUsed, string unit)
    {
        StageId = stageId;
        MaterialName = materialName;
        QuantityUsed = quantityUsed;
        Unit = unit;
        RecordedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int StageId { get; private set; }

    /// <summary>Name of the material, e.g. "Madera de pino", "Barniz".</summary>
    public string MaterialName { get; private set; }

    /// <summary>Amount of the material that was actually used.</summary>
    public decimal QuantityUsed { get; private set; }

    /// <summary>Measurement unit, e.g. "kg", "litros", "metros".</summary>
    public string Unit { get; private set; }

    /// <summary>When the carpenter registered this consumption.</summary>
    public DateTimeOffset RecordedAt { get; private set; }

    // IAuditableEntity
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
