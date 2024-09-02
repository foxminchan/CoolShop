using System.Diagnostics.CodeAnalysis;
using CoolShop.Catalog.Grpc;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using BasketModel = CoolShop.Cart.Domain.Basket;

namespace CoolShop.Cart.Grpc;

public sealed class BasketService(DaprClient daprClient, ILogger<BasketService> logger)
    : AppCallback.AppCallbackBase
{
    public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("[{Service}] - Invoking method: {Method}", nameof(BasketService), request.Method);
        }

        var response = new InvokeResponse();

        switch (request.Method)
        {
            case nameof(Basket.BasketBase.GetBasket):
            {
                var input = request.Data.Unpack<BasketRequest>();

                var basket = await daprClient.GetStateAsync<BasketModel>(ServiceName.Dapr.StateStore, input.Id,
                    cancellationToken: context.CancellationToken);

                var data = await MapToBasketResponse(basket, context.CancellationToken);

                response.Data = Any.Pack(data);

                return response;
            }
            default:
                logger.LogWarning("[{Service}] - Method not found: {Method}", nameof(BasketService), request.Method);
                break;
        }

        return response;
    }

    public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request,
        ServerCallContext context)
    {
        return Task.FromResult(new ListTopicSubscriptionsResponse());
    }

    [DoesNotReturn]
    private static void ThrowNotFound()
    {
        throw new RpcException(new(StatusCode.NotFound, "Basket not found"));
    }

    private async Task<BasketResponse> MapToBasketResponse(BasketModel basket,
        CancellationToken cancellationToken = default)
    {
        var response = new BasketResponse
        {
            Id = basket.AccountId, CouponId = basket.CouponId.ToString(), TotalPrice = 0.0
        };

        foreach (var item in basket.BasketItems)
        {
            var product = await daprClient.InvokeMethodAsync<ProductRequest, ProductResponse>(
                ServiceName.AppId.Catalog,
                nameof(Catalog.Grpc.Product.ProductClient.GetProduct),
                new() { Id = item.Id.ToString() },
                cancellationToken);

            response.Products.Add(new Product
            {
                Id = item.Id.ToString(),
                Name = product.Name,
                Quantity = item.Quantity,
                Price = product.Price,
                Discount = product.Discount
            });
        }

        response.TotalPrice =
            response.Products.Sum(x => x.Discount > 0 ? x.Discount * x.Quantity : x.Price * x.Quantity);

        return response;
    }
}
