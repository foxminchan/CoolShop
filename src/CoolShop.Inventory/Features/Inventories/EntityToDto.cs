using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories;

public static class EntityToDto
{
    public static InventoryDto ToDto(this InventoryModel inventory)
    {
        return new(
            inventory.Id,
            inventory.QuantityAvailable,
            inventory.QuantityOnHold,
            inventory.SupplierId,
            inventory.WarehouseId);
    }

    public static IEnumerable<InventoryDto> ToDtos(this IEnumerable<InventoryModel> inventories)
    {
        return inventories.Select(ToDto);
    }
}
