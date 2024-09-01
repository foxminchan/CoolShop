namespace CoolShop.Catalog.Domain;

public sealed class Category : EntityBase, IAggregateRoot, ISoftDelete
{
    private Category()
    {
        // EF Core
    }

    public Category(string name)
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
