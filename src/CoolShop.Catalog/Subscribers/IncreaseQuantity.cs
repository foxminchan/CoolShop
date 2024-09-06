using CoolShop.Catalog.Application.Products.IncreaseQuantity;
using CoolShop.Catalog.Domain.IntegrationEvents;

namespace CoolShop.Catalog.Subscribers;

public sealed class IncreaseQuantity : ISubscriber<CatalogIncreasedQuantityIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(CatalogIncreasedQuantityIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-increase-quantity",
                async (CatalogIncreasedQuantityIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(CatalogIncreasedQuantityIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new IncreaseQuantityCommand(request.ProductQuantities), cancellationToken);
    }
}
