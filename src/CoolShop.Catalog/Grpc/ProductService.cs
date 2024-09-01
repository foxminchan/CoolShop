using System.Diagnostics.CodeAnalysis;
using CoolShop.Catalog.Application.Products;
using CoolShop.Catalog.Application.Products.Get;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CoolShop.Catalog.Grpc;

public sealed class ProductService(ISender sender, ILogger<ProductService> logger)
    : AppCallback.AppCallbackBase
{
    public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("[{Service}] - Invoking method: {Method}", nameof(ProductService), request.Method);
        }

        var response = new InvokeResponse();

        switch (request.Method)
        {
            case nameof(Product.ProductBase.GetProduct):
            {
                var input = request.Data.Unpack<ProductRequest>();

                var product = await RetrieveProduct(Guid.Parse(input.Id));

                response.Data = Any.Pack(MapToProductResponse(product));

                return response;
            }
            case nameof(Product.ProductBase.GetProductStatus):
            {
                var input = request.Data.Unpack<ProductStatusRequest>();

                var product = await RetrieveProduct(Guid.Parse(input.Id));

                response.Data = Any.Pack(MapToProductStatusResponse(product));

                return response;
            }
            default:
                logger.LogWarning("[{Service}] - Method not found: {Method}", nameof(ProductService), request.Method);
                break;
        }

        return response;
    }

    public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request,
        ServerCallContext context)
    {
        return Task.FromResult(new ListTopicSubscriptionsResponse());
    }

    private async Task<ProductDto> RetrieveProduct(Guid id)
    {
        var product = await sender.Send(new GetProductQuery(id));

        if (product.Value is null)
        {
            ThrowNotFound();
        }

        return product.Value;
    }

    [DoesNotReturn]
    private static void ThrowNotFound()
    {
        throw new RpcException(new(StatusCode.NotFound, "Product not found"));
    }

    private static ProductResponse MapToProductResponse(ProductDto product)
    {
        return new()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Description = product.Description,
            Price = decimal.ToDouble(product.Price),
            Discount = decimal.ToDouble(product.PriceSale)
        };
    }

    private static ProductStatusResponse MapToProductStatusResponse(ProductDto product)
    {
        return new() { Id = product.Id.ToString(), Status = product.Status.ToString() };
    }
}
