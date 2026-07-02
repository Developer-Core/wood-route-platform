using DeveloperCore.WoodRoute.Platform.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Profiles bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Profiles bounded context persistence configuration.
    /// </summary>
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Profiles Context

        // Profile aggregate root
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

        // Person name owned value object, stored in the profiles table
        builder.Entity<Profile>().OwnsOne(p => p.Name,
            name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            });

        // Email address owned value object, unique across profiles
        builder.Entity<Profile>().OwnsOne(p => p.Email,
            email =>
            {
                email.WithOwner().HasForeignKey("Id");
                email.Property(e => e.Address).HasColumnName("EmailAddress").HasMaxLength(320).IsRequired();
                email.HasIndex(e => e.Address).IsUnique();
            });
    }
}
