namespace CoolShop.Cart.Features.Remove;

public sealed class RemoveBasketCommand : ICommand<Result>;

public sealed class DeleteBasketHandler(DaprClient daprClient, IIdentityService identityService)
    : ICommandHandler<RemoveBasketCommand, Result>
{
    public async Task<Result> Handle(RemoveBasketCommand request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        var state = await daprClient.GetStateEntryAsync<BasketDto>(ServiceName.Dapr.StateStore, customerId,
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(customerId, state);

        await state.DeleteAsync(cancellationToken: cancellationToken);

        return Result.Success();
    }
}
