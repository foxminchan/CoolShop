using CoolShop.Cart.IntegrationEvents;

namespace CoolShop.Cart.Features.Delete;

public sealed class DeleteBasketSubscriber : ISubscriber<BasketDeletedIntegrationEvent, ISender>
{
    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub, Name = nameof(BasketDeletedIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-delete-basket",
                async (BasketDeletedIntegrationEvent request, ISender sender) =>
                    await HandleAsync(request, sender))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(BasketDeletedIntegrationEvent request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteBasketCommand(request.BasketId.ToString()), cancellationToken);
    }
}
