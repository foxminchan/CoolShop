namespace CoolShop.Catalog.Domain;

public sealed class Brand : EntityBase, IAggregateRoot, ISoftDelete
{
    private Brand()
    {
        // EF Core
    }

    public Brand(string name)
    {
        Name = Guard.Against.NullOrEmpty(name);
    }

    public string? Name { get; private set; }
    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void Update(string name)
    {
        Name = Guard.Against.NullOrEmpty(name);
    }
}
