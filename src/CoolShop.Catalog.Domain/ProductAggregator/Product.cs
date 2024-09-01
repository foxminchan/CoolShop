namespace CoolShop.Catalog.Domain.ProductAggregator;

public sealed class Product : EntityBase, IAggregateRoot, ISoftDelete
{
    private Product()
    {
        // EF Core
    }

    public Product(string name, string? description, string? image, decimal price,
        decimal priceSale, Status status, Guid categoryId, Guid brandId,
        Guid inventoryId)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = description;
        Image = image;
        Price = new(price, priceSale);
        Status = Guard.Against.EnumOutOfRange(status);
        CategoryId = Guard.Against.Default(categoryId);
        BrandId = brandId;
        InventoryId = inventoryId;
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Image { get; private set; }
    public Price? Price { get; private set; }
    public Status Status { get; private set; }
    public double Rating { get; private set; }
    public long ReviewsCount { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; } = default!;
    public Guid? BrandId { get; private set; }
    public Brand? Brand { get; private set; } = default!;
    public Guid? InventoryId { get; private set; }
    public bool IsDeleted { get; set; }

    public void Update(string name, string? description, string? image, decimal price,
        decimal priceSale, Guid categoryId, Guid brandId, Guid inventoryId)
    {
        Name = Guard.Against.NullOrEmpty(name);
        Description = description;
        Image = image;
        Price = new(price, priceSale);
        CategoryId = Guard.Against.Default(categoryId);
        BrandId = brandId;
        InventoryId = inventoryId;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public void RemoveImage()
    {
        Image = null;
    }

    public void MarkDiscontinued()
    {
        Status = Status.Discontinued;
    }

    public void MarkOutOfStock()
    {
        Status = Status.OutOfStock;
    }

    public void MarkInStock()
    {
        Status = Status.InStock;
    }

    public void AddRating(int rating)
    {
        Rating = ((Rating * ReviewsCount) + rating) / (ReviewsCount + 1);
        ReviewsCount++;
    }

    public void RemoveRating(int rating)
    {
        Rating = ((Rating * ReviewsCount) - rating) / (ReviewsCount - 1);
        ReviewsCount--;
    }
}
