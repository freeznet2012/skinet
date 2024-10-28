using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <inheritdoc />
public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query =  storeContext.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p => p.Brand.Contains(brand));
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p => p.Type.Contains(type));
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            query = sort switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name),
            };
        }

        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<string>> GetBrandsAsync()
    {
        return await storeContext.Products
            .Select(p => p.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<string>> GetTypesAsync()
    {
        return await storeContext.Products
            .Select(p => p.Type)
            .Distinct()
            .ToListAsync();
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
