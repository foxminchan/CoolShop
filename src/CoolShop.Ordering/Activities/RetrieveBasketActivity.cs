using CoolShop.Cart.Grpc;

namespace CoolShop.Ordering.Activities;

public sealed class RetrieveBasketActivity(DaprClient daprClient, ILoggerFactory loggerFactory)
    : WorkflowActivity<Guid, BasketResponse>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RetrieveBasketActivity>();

    public override async Task<BasketResponse> RunAsync(WorkflowActivityContext context, Guid input)
    {
        _logger.LogInformation("[{Activity}] - Retrieving basket for user {UserId}", nameof(RetrieveBasketActivity),
            input);

        var cart = await daprClient.InvokeMethodAsync<BasketRequest, BasketResponse>(
            ServiceName.AppId.Cart,
            nameof(Basket.BasketClient.GetBasket),
            new() { Id = input.ToString() });

        return cart;
    }
}
