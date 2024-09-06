using CoolShop.Catalog.Application.Products.RemoveRating;
using CoolShop.Catalog.Domain.IntegrationEvents;

namespace CoolShop.Catalog.Subscribers;

public sealed class RemoveRating : ISubscriber<CatalogRemovedRatingIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(CatalogRemovedRatingIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-remove-rating",
                async (CatalogRemovedRatingIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(CatalogRemovedRatingIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new RemoveRatingCommand(request.ProductId, request.Rating), cancellationToken);
    }
}
