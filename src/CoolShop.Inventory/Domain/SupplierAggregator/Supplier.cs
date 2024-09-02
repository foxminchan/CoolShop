namespace CoolShop.Inventory.Domain.SupplierAggregator;

public sealed class Supplier : EntityBase, IAggregateRoot
{
    public Supplier()
    {
        // EF Core
    }

    public Supplier(string? name, string? phoneNumber)
    {
        Name = Guard.Against.NullOrEmpty(name);
        PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber);
    }

    public string? Name { get; private set; }
    public string? PhoneNumber { get; private set; }

    public void Update(string? name, string? phoneNumber)
    {
        Name = Guard.Against.NullOrEmpty(name);
        PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber);
    }
}
