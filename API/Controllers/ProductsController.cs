using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// API controller for Products.
/// </summary>
[ApiController]
[Route("api/{controller}")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    /// <summary>
    /// Gets a list of products.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="Product"/> objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProducts()
    {
        return Ok(await productRepository.GetProductsAsync());
    }

    /// <summary>
    /// Gets a product by Id.
    /// </summary>
    /// <param name="id">The Id of the product that needs to be returned.</param>
    /// <returns>An object of class <see cref="Product"/>.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    /// <summary>
    /// Creates a Product.
    /// </summary>
    /// <param name="product">The Product that needs to be created.</param>
    /// <returns>An object of class <see cref="Product"/> which was created.</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        productRepository.AddProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest("Product could not be created");
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

        if (id != product.Id || !productRepository.ProductExists(id))
        {
            return BadRequest("Cannot update this product");
        }

        productRepository.UpdateProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product could not be updated");
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
        var product = await productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        productRepository.DeleteProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product could not be deleted");
    }
}
