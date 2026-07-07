using DeveloperCore.WoodRoute.Platform.Customers.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCore.WoodRoute.Platform.Customers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Customers bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Customers bounded context persistence configuration.
    /// </summary>
    public static void ApplyCustomersConfiguration(this ModelBuilder builder)
    {
        // Customers Context

        // Customer aggregate root
        builder.Entity<Customer>().HasKey(c => c.Id);
        builder.Entity<Customer>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();

        // Contact fields stored directly on the customers table
        builder.Entity<Customer>().Property(c => c.Phone).HasColumnName("Phone").HasMaxLength(30).IsRequired();
        builder.Entity<Customer>().Property(c => c.UserId).HasColumnName("UserId").IsRequired(false);

        // Person name owned value object, stored in the customers table
        builder.Entity<Customer>().OwnsOne(c => c.Name,
            name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            });

        // Email address owned value object, unique across customers
        builder.Entity<Customer>().OwnsOne(c => c.Email,
            email =>
            {
                email.WithOwner().HasForeignKey("Id");
                email.Property(e => e.Address).HasColumnName("EmailAddress").HasMaxLength(320).IsRequired();
                email.HasIndex(e => e.Address).IsUnique();
            });
    }
}
