using CoolShop.Catalog.Domain.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Application.Products.Activities;

public sealed class IncreaseInventoryActivity(DaprClient daprClient, ILoggerFactory loggerFactory)
    : WorkflowActivity<Dictionary<Guid, int>, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<IncreaseInventoryActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Dictionary<Guid, int> input)
    {
        _logger.LogInformation("[{Activity}] - Updating inventory for products", nameof(IncreaseInventoryActivity));

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(InventoryIncreasedQuantityIntegrationEvent).ToLowerInvariant(),
            new InventoryIncreasedQuantityIntegrationEvent(input));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(IncreaseInventoryActivity),
            nameof(InventoryIncreasedQuantityIntegrationEvent));

        return default;
    }
}
