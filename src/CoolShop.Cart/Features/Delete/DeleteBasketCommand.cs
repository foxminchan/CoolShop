using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Constants;
using CoolShop.Core.SharedKernel;
using CoolShop.Shared.Identity;
using Dapr.Client;

namespace CoolShop.Cart.Features.Delete;

public sealed class DeleteBasketCommand : ICommand<Result>;

public sealed class DeleteBasketHandler(DaprClient daprClient, IIdentityService identityService)
    : ICommandHandler<DeleteBasketCommand, Result>
{
    public async Task<Result> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
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
