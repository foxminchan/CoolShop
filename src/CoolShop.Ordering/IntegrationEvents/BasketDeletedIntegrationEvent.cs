﻿namespace CoolShop.Ordering.IntegrationEvents;

public sealed class BasketDeletedIntegrationEvent(Guid basketId) : IntegrationEvent
{
    public Guid BasketId { get; init; } = Guard.Against.Default(basketId);
}
