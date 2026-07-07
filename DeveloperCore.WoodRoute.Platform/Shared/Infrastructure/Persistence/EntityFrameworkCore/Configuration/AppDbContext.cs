using DeveloperCore.WoodRoute.Platform.Customers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Engagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the WoodRoute Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Bounded context configurations will be applied here as each context is added.
        builder.ApplySalesConfiguration();
        builder.ApplyInventoryConfiguration();
        builder.ApplyEngagementConfiguration();
        builder.ApplyManufacturingConfiguration();
        builder.ApplyProfilesConfiguration();
        builder.ApplyCustomersConfiguration();
        builder.ApplyIamConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
