using DeveloperCore.WoodRoute.Platform.Iam.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Iam bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Iam bounded context persistence configuration.
    /// </summary>
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // Iam Context

        // User aggregate root
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Email).HasMaxLength(320).IsRequired();
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Role).HasConversion<string>().HasMaxLength(40).IsRequired();
    }
}
