using CoolShop.Catalog.Domain.ProductAggregator;
using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Application.Products.Activities;

public sealed class RetrieveInventoryActivity(IReadRepository<Product> repository, ILoggerFactory loggerFactory)
    : WorkflowActivity<Dictionary<Guid, int>, Dictionary<Guid, int>>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RetrieveInventoryActivity>();

    public override async Task<Dictionary<Guid, int>> RunAsync(WorkflowActivityContext context,
        Dictionary<Guid, int> input)
    {
        _logger.LogInformation("[{Activity}] - Retrieving inventory for products", nameof(RetrieveInventoryActivity));

        Dictionary<Guid, Guid?> inventories = new();

        foreach (var productId in input.Keys)
        {
            var product = await repository.GetByIdAsync(productId);

            if (product?.InventoryId is not null)
            {
                inventories.Add(productId, product.InventoryId);
            }
        }

        var inventoryQuantities = input
            .Select(x => new KeyValuePair<Guid, int>(inventories[x.Key]!.Value, x.Value))
            .ToDictionary(x => x.Key, x => x.Value);

        return inventoryQuantities;
    }
}
