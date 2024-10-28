// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext storeContext)
    {
        if (!storeContext.Products.Any())
        {
            var produxtSeed = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(produxtSeed);
            if (products == null)
            {
                return;
            }

            storeContext.Products.AddRange(products);
            await storeContext.SaveChangesAsync();

        }
    }
}
