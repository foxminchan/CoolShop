namespace CoolShop.Rating.IntegrationEvents;

public sealed class CatalogRemovedRatingIntegrationEvent(Guid productId, int rating) : IntegrationEvent
{
    public Guid ProductId { get; init; } = Guard.Against.Default(productId);
    public int Rating { get; init; } = Guard.Against.OutOfRange(rating, nameof(rating), 0, 5);
}
