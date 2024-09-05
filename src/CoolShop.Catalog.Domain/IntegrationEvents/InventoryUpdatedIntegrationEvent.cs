﻿namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class InventoryUpdatedIntegrationEvent(Dictionary<Guid, bool> isOutOfStock)
{
    public Dictionary<Guid, bool> IsOutOfStock { get; init; } = isOutOfStock;
}
