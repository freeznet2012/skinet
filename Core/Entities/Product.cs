namespace Core.Entities;

/// <summary>
/// Represents a product entity with properties like Id, Name, Price, PictureUrl, Type, Brand, and QuantityInStock.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// This field is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the URL of the product's picture.
    /// This field is required.
    /// </summary>
#pragma warning disable CA1056 // URI-like properties should not be strings
    public required string PictureUrl { get; set; }
#pragma warning restore CA1056 // URI-like properties should not be strings

    /// <summary>
    /// Gets or sets the type or category of the product.
    /// This field is required.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Gets or sets the brand of the product.
    /// This field is required.
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product available in stock.
    /// </summary>
    public int QuantityInStock { get; set; }
}
