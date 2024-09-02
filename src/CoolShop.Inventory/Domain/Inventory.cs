using CoolShop.Inventory.Domain.SupplierAggregator;
using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Domain;

public sealed class Inventory : EntityBase, IAggregateRoot, ISoftDelete
{
    public Inventory()
    {
        // EF Core
    }

    public Inventory(int quantityAvailable, int quantityOnHold, Guid? supplierId, Guid? warehouseId)
    {
        QuantityAvailable = Guard.Against.OutOfRange(quantityAvailable, nameof(quantityAvailable), 0, int.MaxValue);
        QuantityOnHold = Guard.Against.OutOfRange(quantityOnHold, nameof(quantityOnHold), 0, int.MaxValue);
        SupplierId = supplierId;
        WarehouseId = warehouseId;
    }

    public int QuantityAvailable { get; private set; }
    public int QuantityOnHold { get; private set; }
    public Guid? SupplierId { get; private set; }
    public Supplier? Supplier { get; }
    public Guid? WarehouseId { get; private set; }
    public Warehouse? Warehouse { get; }
    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }
}
