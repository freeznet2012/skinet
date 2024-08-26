using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

/// <summary>
/// Configuration class for the <see cref="Product"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface to configure entity properties.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the properties of the <see cref="Product"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="builder"/> is null.
    /// </exception>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Configures the Price property with a specific SQL column type.
        builder.Property(x => x.Price).HasColumnType("decimal(18.2)");

        // Configures the Name property to be required (not nullable).
        builder.Property(x => x.Name).IsRequired();
    }
}
