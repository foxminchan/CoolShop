using CoolShop.Inventory.Domain.SupplierAggregator;
using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Infrastructure.Data;

public sealed class InventoryContextSeed : IDbSeeder<InventoryContext>
{
    public async Task SeedAsync(InventoryContext context)
    {
        if (!context.Warehouses.Any())
        {
            await context.Warehouses.AddRangeAsync(GetPreconfiguredWarehouses());
            await context.SaveChangesAsync();
        }

        if (!context.Suppliers.Any())
        {
            await context.Suppliers.AddRangeAsync(GetPreconfiguredSuppliers());
            await context.SaveChangesAsync();
        }

        if (!context.Inventories.Any())
        {
            var suppliers = context.Suppliers.ToList();
            var warehouses = context.Warehouses.ToList();

            foreach (var supplier in suppliers)
            {
                foreach (var warehouse in warehouses)
                {
                    await context.Inventories.AddAsync(new(new Random().Next(200, 500), new Random().Next(50, 100), supplier.Id, warehouse.Id));
                }
            }

            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
    }

    private static IEnumerable<Warehouse> GetPreconfiguredWarehouses()
    {
        return new List<Warehouse>
        {
            new("Thu Duc Warehouse", "Xa Lo Ha Noi, Thu Duc District, Ho Chi Minh City", 2000),
            new("Tan Binh Warehouse", "Cong Hoa, Tan Binh District, Ho Chi Minh City", 1500),
            new("Binh Thanh Warehouse", "Dinh Bo Linh, Binh Thanh District, Ho Chi Minh City", 1000),
            new("District 1 Warehouse", "Le Loi, District 1, Ho Chi Minh City", 500),
            new("District 2 Warehouse", "Nguyen Van Huong, District 2, Ho Chi Minh City", 300),
            new("Hoc Mon Warehouse", "Quoc Lo 22, Hoc Mon District, Ho Chi Minh City", 100),
            new("Cu Chi Warehouse", "Quoc Lo 22, Cu Chi District, Ho Chi Minh City", 500),
            new("District 7 Warehouse", "Nguyen Van Linh, District 7, Ho Chi Minh City", 300)
        };
    }

    private static IEnumerable<Supplier> GetPreconfiguredSuppliers()
    {
        return new List<Supplier>
        {
            new("Nike", "0123456789"),
            new("Adidas", "0123456789"),
            new("Puma", "0123456789"),
            new("Uniqlo", "0123456789"),
            new("Zara", "0123456789"),
            new("H&M", "0123456789"),
            new("Gucci", "0123456789")
        };
    }
}
