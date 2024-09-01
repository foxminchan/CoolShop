using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Cart.Domain;
using CoolShop.Constants;
using CoolShop.Core.SharedKernel;
using CoolShop.Shared.Identity;
using Dapr.Client;

namespace CoolShop.Cart.Features.ReduceItemQuantity;

public sealed record ReduceItemQuantityCommand(Guid ProductId, int Quantity) : ICommand<Result>;

public sealed class ReduceItemQuantityHandler(DaprClient daprClient, IIdentityService identityService)
    : ICommandHandler<ReduceItemQuantityCommand, Result>
{
    public async Task<Result> Handle(ReduceItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        var state = await daprClient.GetStateEntryAsync<Basket>(ServiceName.Dapr.StateStore, customerId,
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(customerId, state);

        var basket = state.Value;

        basket.ReduceItemQuantity(request.ProductId, request.Quantity);

        await state.SaveAsync(cancellationToken: cancellationToken);

        return Result.Success();
    }
}
