using CoolShop.Cart.IntegrationEvents;

namespace CoolShop.Cart.Features.Delete;

public sealed record DeleteBasketCommand(string BasketId) : ICommand<Result>;

public sealed class DeleteBasketCommandHandler(DaprClient daprClient) : ICommandHandler<DeleteBasketCommand, Result>
{
    public async Task<Result> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var state = await daprClient.GetStateEntryAsync<BasketDto>(ServiceName.Dapr.StateStore, request.BasketId,
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(request.BasketId, state);

        await state.DeleteAsync(cancellationToken: cancellationToken);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(BasketUpdatedIntegrationEvent).ToLowerInvariant(),
            new BasketUpdatedIntegrationEvent(),
            cancellationToken);

        return Result.Success();
    }
}
