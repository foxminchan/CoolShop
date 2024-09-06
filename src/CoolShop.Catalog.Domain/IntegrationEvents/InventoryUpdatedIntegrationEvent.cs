namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class InventoryUpdatedIntegrationEvent(Dictionary<Guid, bool> productStatus) : IntegrationEvent
{
    public Dictionary<Guid, bool> ProductStatus { get; init; } = productStatus;
}
