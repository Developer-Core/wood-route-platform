using DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Manufacturing bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies all Manufacturing entity configurations to the model builder.
    /// </summary>
    public static void ApplyManufacturingConfiguration(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ManufactureOrderEntityConfiguration());
        builder.ApplyConfiguration(new StageEntityConfiguration());
    }
}
