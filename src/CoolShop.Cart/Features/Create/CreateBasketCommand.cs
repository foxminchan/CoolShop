namespace CoolShop.Cart.Features.Create;

public sealed record CreateBasketCommand(Guid ProductId, Guid? CouponId, int Quantity) : ICommand<Result<string>>;

public sealed class CreateBasketHandler(DaprClient daprClient, IIdentityService identityService)
    : ICommandHandler<CreateBasketCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        Basket basket = new(customerId, request.CouponId, [new(request.ProductId, request.Quantity)]);

        var existingBasket = await daprClient.GetStateEntryAsync<Basket>(
            ServiceName.Dapr.StateStore,
            customerId,
            cancellationToken: cancellationToken);

        if (existingBasket.Value is not null)
        {
            existingBasket.Value.AddItem(new(request.ProductId, request.Quantity));
            await existingBasket.SaveAsync(cancellationToken: cancellationToken);
            return existingBasket.Value.AccountId;
        }

        await daprClient.SaveStateAsync(ServiceName.Dapr.StateStore, customerId, basket,
            cancellationToken: cancellationToken);

        return basket.AccountId;
    }
}
