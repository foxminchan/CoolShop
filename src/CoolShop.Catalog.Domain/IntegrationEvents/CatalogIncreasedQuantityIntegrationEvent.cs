namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class CatalogIncreasedQuantityIntegrationEvent(Dictionary<Guid, int> productQuantities) : IntegrationEvent
{
    public Dictionary<Guid, int> ProductQuantities { get; init; } = productQuantities;
}
