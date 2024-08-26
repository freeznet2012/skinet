using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

/// <summary>
/// API controller for Products.
/// </summary>
[ApiController]
[Route("api/{controller}")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class with the specified logger.
    /// </summary>
    /// <param name="context">The db context instance used for store.</param>

    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of products.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="Product"/> objects.</returns>
    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Gets a product by Id.
    /// </summary>
    /// <param name="id">The Id of the product that needs to be returned.</param>
    /// <returns>An object of class <see cref="Product"/>.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    /// <summary>
    /// Creates a Product
    /// </summary>
    /// <param name="product">The Product that needs to be created.</param>
    /// <returns>An object of class <see cref="Product"/> which was created.</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="id">The ID of the product to be updated.</param>
    /// <param name="product">The product object with updated values.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> representing the result of the operation:
    /// <list type="bullet">
    /// <item><description><see cref="BadRequest"/> if the product ID does not match or the product does not exist.</description></item>
    /// <item><description><see cref="NoContent"/> if the product is successfully updated.</description></item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="product"/> is null.</exception>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (id != product.Id || !ProductExists(id))
        {
            return BadRequest("Cannot update this product");
        }

        _context.Entry(product).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes an existing product from the database.
    /// </summary>
    /// <param name="id">The ID of the product to be deleted.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> representing the result of the operation:
    /// <list type="bullet">
    /// <item><description><see cref="NotFound"/> if the product with the specified ID does not exist.</description></item>
    /// <item><description><see cref="NoContent"/> if the product is successfully deleted.</description></item>
    /// </list>
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool ProductExists(int id)
    {
        return _context.Products.Any(x => x.Id == id);
    }
}
