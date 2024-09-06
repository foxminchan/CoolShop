using CoolShop.Catalog.Application.Products.AddRating;
using CoolShop.Catalog.Domain.IntegrationEvents;

namespace CoolShop.Catalog.Subscribers;

public sealed class AddRating : ISubscriber<CatalogAddedRatingIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub,
            Name = nameof(CatalogAddedRatingIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-add-rating",
                async (CatalogAddedRatingIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(CatalogAddedRatingIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new AddRatingCommand(request.ProductId, request.Rating), cancellationToken);
    }
}
