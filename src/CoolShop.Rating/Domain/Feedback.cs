namespace CoolShop.Rating.Domain;

public sealed class Feedback(Guid productId, int rating, string? comment, Guid userId) : IAggregateRoot
{
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public int Rating { get; private set; } = Guard.Against.OutOfRange(rating, nameof(rating), 0, 5);
    public string? Comment { get; private set; } = comment;
    public Guid ProductId { get; private set; } = Guard.Against.Default(productId);
    public Guid UserId { get; private set; } = Guard.Against.Default(userId);
    public bool IsHidden { get; private set; }

    public void Hide()
    {
        IsHidden = true;
    }
}
