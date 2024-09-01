using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Cart.Domain;
using CoolShop.Constants;
using CoolShop.Core.SharedKernel;
using CoolShop.Shared.Identity;
using Dapr.Client;

namespace CoolShop.Cart.Features.RemoveItem;

public sealed record RemoveItemCommand(Guid ProductId) : ICommand<Result>;

public sealed class RemoveItemHandler(DaprClient daprClient, IIdentityService identityService)
    : ICommandHandler<RemoveItemCommand, Result>
{
    public async Task<Result> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        var state = await daprClient.GetStateEntryAsync<Basket>(ServiceName.Dapr.StateStore, customerId,
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(customerId, state);

        var basket = state.Value;

        basket.RemoveItem(request.ProductId);

        await state.SaveAsync(cancellationToken: cancellationToken);

        return Result.Success();
    }
}
