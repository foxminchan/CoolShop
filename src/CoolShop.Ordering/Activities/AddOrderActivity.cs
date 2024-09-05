using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Activities;

public sealed class AddOrderActivity(IRepository<Order> repository, ILoggerFactory loggerFactory, DaprClient daprClient)
    : WorkflowActivity<AddOrderActivityData, AddOrderActivityResult>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<AddOrderActivity>();

    public override async Task<AddOrderActivityResult> RunAsync(WorkflowActivityContext context,
        AddOrderActivityData input)
    {
        _logger.LogInformation("[{Activity}] - Adding order for user {UserId}", nameof(AddOrderActivity),
            input.BuyerId);

        var order = new Order(input.Note, input.PaymentMethod, input.BuyerId);

        foreach (var item in input.Items)
        {
            order.AddOrderItem(item.ProductId, item.Quantity, item.Price);
        }

        var productQuantities = input.Items.ToDictionary(x => x.ProductId, x => x.Quantity);

        Order result;

#pragma warning disable CS0618 // Distributed Lock API is in Alpha, this can be removed once it is stable
        await using (var block = await daprClient.Lock(ServiceName.Dapr.LockStore, order.Id.ToString(),
                         order.BuyerId.ToString(),
                         20))
#pragma warning restore CS0618
        {
            if (block.Success)
            {
                result = await repository.AddAsync(order);
            }
            else
            {
                _logger.LogWarning("[{Activity}] - Failed to acquire lock for order {OrderId} and user {UserId}",
                    nameof(AddOrderActivity), order.Id, order.BuyerId);

                throw new InvalidOperationException("Failed to acquire lock for order");
            }
        }

        OrderingTraceExtension.LogOrderCreated(_logger, nameof(AddOrderActivity), result.Id);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(BasketDeletedIntegrationEvent).ToLowerInvariant(),
            new BasketDeletedIntegrationEvent(result.Id));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(AddOrderActivity),
            nameof(BasketDeletedIntegrationEvent));

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogReducedQuantityIntegrationEvent).ToLowerInvariant(),
            new CatalogReducedQuantityIntegrationEvent(productQuantities));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(AddOrderActivity),
            nameof(CatalogReducedQuantityIntegrationEvent));

        return new(result.Id, true);
    }
}
