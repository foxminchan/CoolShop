using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Infrastructure.Data;

public sealed class CatalogContextSeed : IDbSeeder<CatalogContext>
{
    public async Task SeedAsync(CatalogContext context)
    {
        if (!context.Brands.Any())
        {
            await context.Brands.AddRangeAsync(GetPreconfiguredBrands());
            await context.SaveChangesAsync();
        }

        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(GetPreconfiguredCategories());
            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
    }

    private static IEnumerable<Brand> GetPreconfiguredBrands()
    {
        return new List<Brand>
        {
            new("Nike"),
            new("Adidas"),
            new("Puma"),
            new("Uniqlo"),
            new("Zara"),
            new("H&M")
        };
    }

    private static IEnumerable<Category> GetPreconfiguredCategories()
    {
        return new List<Category>
        {
            new("Shoes"),
            new("T-Shirts"),
            new("Pants"),
            new("Dresses"),
            new("Skirts"),
            new("Sweaters")
        };
    }
}
