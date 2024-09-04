using Bogus;
using CoolShop.Catalog.Domain.ProductAggregator;
using Dapr.Client;

namespace CoolShop.Catalog.Infrastructure.Data;

public sealed class CatalogContextSeed(DaprClient daprClient) : IDbSeeder<CatalogContext>
{
    private readonly Faker _faker = new();

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

        if (!context.Products.Any())
        {
            var brands = context.Brands.ToList();
            var categories = context.Categories.ToList();

            var inventory = await daprClient.InvokeMethodAsync<InventoryResponse>(HttpMethod.Get,
                ServiceName.AppId.Inventory, "/api/v1/inventories");

            for (var i = 0; i < 100; i++)
            {
                var brand = brands[_faker.Random.Int(0, brands.Count - 1)];
                var category = categories[_faker.Random.Int(0, categories.Count - 1)];
                var inventoryItem = inventory.Inventories[_faker.Random.Int(0, inventory.Inventories.Count - 1)];

                var product = new Product(
                    _faker.Commerce.ProductName(),
                    _faker.Commerce.ProductDescription(),
                    null,
                    _faker.Random.Decimal(100, 1000),
                    _faker.Random.Decimal(1, 100),
                    _faker.PickRandom<Status>(),
                    category.Id,
                    brand.Id,
                    inventoryItem.Id);

                await context.Products.AddAsync(product);
            }

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

internal sealed record InventoryResponse(PagedInfo PagedInfo, List<InventoryDto> Inventories);

internal sealed record InventoryDto(
    Guid Id,
    int QuantityAvailable,
    int QuantityOnHold,
    Guid? SupplierId,
    Guid? WarehouseId);
