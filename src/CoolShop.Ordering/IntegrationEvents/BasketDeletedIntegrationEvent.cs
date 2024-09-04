namespace CoolShop.Ordering.IntegrationEvents;

public sealed class BasketDeletedIntegrationEvent(Guid basketId)
{
    public Guid BasketId { get; init; } = Guard.Against.Default(basketId);
}
