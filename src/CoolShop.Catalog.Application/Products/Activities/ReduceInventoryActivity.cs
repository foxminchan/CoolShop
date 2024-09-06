using CoolShop.Catalog.Domain.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Application.Products.Activities;

public sealed class ReduceInventoryActivity(DaprClient daprClient, ILoggerFactory loggerFactory)
    : WorkflowActivity<Dictionary<Guid, int>, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ReduceInventoryActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Dictionary<Guid, int> input)
    {
        _logger.LogInformation("[{Activity}] - Updating inventory for products", nameof(ReduceInventoryActivity));

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(InventoryReducedQuantityIntegrationEvent).ToLowerInvariant(),
            new InventoryReducedQuantityIntegrationEvent(input));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(ReduceInventoryActivity),
            nameof(InventoryReducedQuantityIntegrationEvent));

        return default;
    }
}
