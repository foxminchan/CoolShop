using CoolShop.Catalog.Domain.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Application.Products.Activities;

public sealed class UpdateInventoryActivity(DaprClient daprClient, ILoggerFactory loggerFactory)
    : WorkflowActivity<Dictionary<Guid, int>, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UpdateInventoryActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Dictionary<Guid, int> input)
    {
        _logger.LogInformation("[{Activity}] - Updating inventory for products", nameof(UpdateInventoryActivity));

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(InventoryReducedQuantityIntegrationEvent).ToLowerInvariant(),
            new InventoryReducedQuantityIntegrationEvent(input));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(UpdateInventoryActivity),
            nameof(InventoryReducedQuantityIntegrationEvent));

        return default;
    }
}
