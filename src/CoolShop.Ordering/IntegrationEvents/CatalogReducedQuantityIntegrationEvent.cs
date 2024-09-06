namespace CoolShop.Ordering.IntegrationEvents;

public sealed class CatalogReducedQuantityIntegrationEvent(Dictionary<Guid, int> productQuantities) : IntegrationEvent
{
    public Dictionary<Guid, int> ProductQuantities { get; init; } = productQuantities;
}
