﻿namespace CoolShop.Catalog.Domain.IntegrationEvents;

public sealed class CatalogReducedQuantityIntegrationEvent(Dictionary<Guid, int> productQuantities)
{
    public Dictionary<Guid, int> ProductQuantities { get; init; } = productQuantities;
}