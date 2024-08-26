using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Represents the database context for the store, providing access to the products in the database.
/// </summary>
public class StoreContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> of <see cref="Product"/> entities,
    /// representing the products available in the store.
    /// </summary>
    public required DbSet<Product> Products { get; set; }

    /// <summary>
    /// Configures the model that is used by the context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="modelBuilder"/> is null.
    /// </exception>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ArgumentNullException.ThrowIfNull(modelBuilder);

        // Applies all configurations in the assembly where ProductConfiguration is located.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
