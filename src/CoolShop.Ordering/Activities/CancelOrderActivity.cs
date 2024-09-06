using CoolShop.Ordering.Domain.OrderAggregate;
using CoolShop.Ordering.Domain.OrderAggregate.Specifications;

namespace CoolShop.Ordering.Activities;

public sealed class CancelOrderActivity(
    IRepository<Order> repository,
    DaprClient daprClient,
    ILoggerFactory loggerFactory) : WorkflowActivity<Guid, CancelOrderActivityResult>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CancelOrderActivity>();

    public override async Task<CancelOrderActivityResult> RunAsync(WorkflowActivityContext context, Guid input)
    {
        _logger.LogInformation("[{Activity}] - Retrieving order with id {OrderId}", nameof(CancelOrderActivity), input);

        var order = await repository.FirstOrDefaultAsync(new OrderFilterSpec(input));

        Guard.Against.NotFound(input, order);

        var productQuantities = order.OrderItems
            .ToDictionary(x => x.ProductId, x => x.Quantity);

        order.MarkAsCancelled();

        await repository.SaveChangesAsync();

        OrderingTraceExtension.LogOrderCancelled(_logger, nameof(CancelOrderActivity), input);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogIncreasedQuantityIntegrationEvent).ToLowerInvariant(),
            new CatalogIncreasedQuantityIntegrationEvent(productQuantities));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(CancelOrderActivity),
            nameof(CatalogIncreasedQuantityIntegrationEvent));

        return new(true, order.Buyer?.Email);
    }
}
