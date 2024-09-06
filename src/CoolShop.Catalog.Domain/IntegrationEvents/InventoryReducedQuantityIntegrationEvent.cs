namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class InventoryReducedQuantityIntegrationEvent(Dictionary<Guid, int> inventoryQuantities)
    : IntegrationEvent
{
    public Dictionary<Guid, int> InventoryQuantities { get; init; } = inventoryQuantities;
}
