using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Domain.BuyerAggregate;

public sealed class Buyer : EntityBase, IAggregateRoot, ISoftDelete
{
    private readonly List<Order> _orders = [];

    private Buyer()
    {
        // EF Core
    }

    public Buyer(Guid id, string name, string? street, string? city, string? province)
    {
        Id = Guard.Against.Default(id);
        Name = Guard.Against.NullOrEmpty(name);
        Address = new(street, city, province);
    }

    public string? Name { get; private set; }
    public Address? Address { get; private set; }

    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }
}
