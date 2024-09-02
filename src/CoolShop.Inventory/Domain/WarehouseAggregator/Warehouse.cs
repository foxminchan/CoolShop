namespace CoolShop.Inventory.Domain.WarehouseAggregator;

public sealed class Warehouse : EntityBase, IAggregateRoot
{
    public Warehouse()
    {
        // EF Core
    }

    public Warehouse(string? name, string? location, int capacity)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Location = Guard.Against.NullOrEmpty(location);
        Capacity = Guard.Against.OutOfRange(capacity, nameof(capacity), 100, int.MaxValue);
    }

    public string? Name { get; private set; }
    public string? Location { get; private set; }
    public int Capacity { get; private set; }

    public void Update(string? name, string? location, int capacity)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Location = Guard.Against.NullOrEmpty(location);
        Capacity = Guard.Against.OutOfRange(capacity, nameof(capacity), 100, int.MaxValue);
    }
}
