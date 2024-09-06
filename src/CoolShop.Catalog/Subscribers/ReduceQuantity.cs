using CoolShop.Catalog.Application.Products.ReduceQuantity;
using CoolShop.Catalog.Domain.IntegrationEvents;

namespace CoolShop.Catalog.Subscribers;

public sealed class ReduceQuantity : ISubscriber<CatalogReducedQuantityIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(CatalogReducedQuantityIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-reduce-quantity",
                async (CatalogReducedQuantityIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(CatalogReducedQuantityIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new ReduceQuantityCommand(request.ProductQuantities), cancellationToken);
    }
}
