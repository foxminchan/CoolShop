namespace CoolShop.Inventory.Features.Inventories;

public sealed record InventoryDto(
    Guid Id,
    int QuantityAvailable,
    int QuantityOnHold,
    Guid? SupplierId,
    Guid? WarehouseId);
