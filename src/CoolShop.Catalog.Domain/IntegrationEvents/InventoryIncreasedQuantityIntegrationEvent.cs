namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class InventoryIncreasedQuantityIntegrationEvent(Dictionary<Guid, int> inventoryQuantities)
    : IntegrationEvent
{
    public Dictionary<Guid, int> InventoryQuantities { get; init; } = inventoryQuantities;
}
