using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Cart.Domain;
using CoolShop.Catalog.Grpc;
using CoolShop.Constants;
using CoolShop.Core.SharedKernel;
using CoolShop.Shared.Identity;
using Dapr.Client;

namespace CoolShop.Cart.Features.Get;

public sealed record GetBasketQuery : IQuery<Result<BasketDto>>;

public sealed class GetBasketHandler(DaprClient daprClient, IIdentityService identityService)
    : IQueryHandler<GetBasketQuery, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        var state = await daprClient.GetStateEntryAsync<Basket>(ServiceName.Dapr.StateStore, customerId,
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(customerId, state);

        var basket = state.Value;

        return await MapToBasketDto(basket, cancellationToken);
    }

    private async Task<BasketDto> MapToBasketDto(Basket basket, CancellationToken cancellationToken)
    {
        var basketDto = new BasketDto(basket.AccountId, basket.CouponId, [], 0.0m);

        foreach (var item in basket.BasketItems)
        {
            var product = await daprClient.InvokeMethodAsync<ProductRequest, ProductResponse>(
                ServiceName.AppId.Catalog,
                nameof(Product.ProductClient.GetProduct),
                new() { Id = item.Id.ToString() },
                cancellationToken);

            basketDto.Items.Add(new(item.Id, product.Name, item.Quantity, (decimal)product.Price,
                (decimal)product.Discount));
        }

        basketDto = basketDto with
        {
            TotalPrice = basketDto.Items.Sum(x => x.PriceSale > 0 ? x.PriceSale * x.Quantity : x.Price * x.Quantity)
        };

        return basketDto;
    }
}
