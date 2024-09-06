using CoolShop.Inventory.IntegrationEvents;
using Dapr;

namespace CoolShop.Inventory.Features.Inventories.IncreaseQuantityAvailable;

public sealed class IncreaseQuantityAvailableSubscriber
    : ISubscriber<InventoryIncreasedQuantityIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(InventoryIncreasedQuantityIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-increase-inventory-quantity",
                async (InventoryIncreasedQuantityIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(InventoryIncreasedQuantityIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new IncreaseQuantityAvailableCommand(request.InventoryQuantities), cancellationToken);
    }
}
