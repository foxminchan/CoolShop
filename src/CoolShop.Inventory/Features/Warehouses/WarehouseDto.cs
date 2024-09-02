namespace CoolShop.Inventory.Features.Warehouses;

public sealed record WarehouseDto(Guid Id, string? Name, string? Location, int Capacity);
