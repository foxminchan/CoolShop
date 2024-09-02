using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses;

public static class EntityToDto
{
    public static WarehouseDto ToDto(this Warehouse warehouse)
    {
        return new(warehouse.Id, warehouse.Name, warehouse.Location, warehouse.Capacity);
    }

    public static IEnumerable<WarehouseDto> ToDtos(this IEnumerable<Warehouse> warehouses)
    {
        return warehouses.Select(ToDto);
    }
}
