using CoolShop.Catalog.Domain.IntegrationEvents;
using CoolShop.Catalog.Domain.ProductAggregator;
using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Application.Products.Activities;

public sealed class SetInStockProductActivity(
    DaprClient daprClient,
    IRepository<Product> repository,
    ILoggerFactory loggerFactory) : WorkflowActivity<Dictionary<Guid, bool>, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SetInStockProductActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Dictionary<Guid, bool> input)
    {
        _logger.LogInformation("[{Activity}] - Setting in stock for products", nameof(SetInStockProductActivity));

        var productIds = input.Keys.ToList();
        var productTasks = productIds.Select(id => repository.GetByIdAsync(id)).ToArray();
        var products = await Task.WhenAll(productTasks);
        var validProducts = products
            .Where(product => product is not null && product.Status == Status.OutOfStock)
            .ToList();

        if (!validProducts.Any())
        {
            _logger.LogWarning("[{Activity}] - No products found", nameof(SetInStockProductActivity));
            return default;
        }

        validProducts.ForEach(product => product!.MarkInStock());

        await repository.UpdateRangeAsync(validProducts!);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogUpdatedIntegrationEvent).ToLowerInvariant(),
            new CatalogUpdatedIntegrationEvent());

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(SetInStockProductActivity),
            nameof(CatalogUpdatedIntegrationEvent));

        return default;
    }
}
