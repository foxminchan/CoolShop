namespace CoolShop.Promotion.Domain;

public sealed class Coupon(decimal discount, DateOnly validFrom, DateOnly validTo, string code, Guid[]? productIds)
    : IAggregateRoot, ISoftDelete
{
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public string Code { get; private set; } = Guard.Against.NullOrEmpty(code, nameof(code));

    public decimal Discount { get; private set; }
        = Guard.Against.OutOfRange(discount, nameof(discount), 5, 100);

    public DateOnly ValidFrom { get; private set; } = Guard.Against.Default(validFrom);
    public DateOnly ValidTo { get; private set; } = Guard.Against.Default(validTo);
    public Guid[]? ProductIds { get; private set; } = productIds;

    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = true;
    }
}
