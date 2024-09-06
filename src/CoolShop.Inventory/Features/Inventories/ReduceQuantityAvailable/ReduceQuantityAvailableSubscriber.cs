using CoolShop.Inventory.IntegrationEvents;
using Dapr;

namespace CoolShop.Inventory.Features.Inventories.ReduceQuantityAvailable;

public sealed class ReduceQuantityAvailableSubscriber : ISubscriber<InventoryReducedQuantityIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(InventoryReducedQuantityIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-reduce-inventory-quantity",
                async (InventoryReducedQuantityIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(InventoryReducedQuantityIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new ReduceQuantityAvailableCommand(request.InventoryQuantities);

        await sender.Send(command, cancellationToken);
    }
}
