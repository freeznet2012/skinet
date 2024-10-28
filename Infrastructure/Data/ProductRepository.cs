using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <inheritdoc />
public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
    {
        return await storeContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        storeContext.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        storeContext.Products.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        storeContext.Products.Remove(product);
    }

    public bool ProductExists(int id)
    {
        return storeContext.Products.Any(e => e.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }
}
