using CoolShop.Inventory.IntegrationEvents;
using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.ReduceQuantityAvailable;

public sealed record ReduceQuantityAvailableCommand(Dictionary<Guid, int> InventoryQuantities)
    : ICommand<Result>;

public sealed class ReduceQuantityAvailableHandler(IRepository<InventoryModel> repository, DaprClient daprClient)
    : ICommandHandler<ReduceQuantityAvailableCommand, Result>
{
    public async Task<Result> Handle(ReduceQuantityAvailableCommand request, CancellationToken cancellationToken)
    {
        var inventories = await repository.ListAsync(cancellationToken);

        inventories.ForEach(inventory =>
        {
            if (request.InventoryQuantities.TryGetValue(inventory.Id, out var quantity))
            {
                inventory.ReduceQuantityAvailable(quantity);
            }
        });

        var isOutOfStock = inventories
            .Where(inventory => inventory.QuantityAvailable == 0)
            .ToDictionary(inventory => inventory.Id, _ => true);

        await repository.SaveChangesAsync(cancellationToken);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(InventoryUpdatedIntegrationEvent).ToLowerInvariant(),
            new InventoryUpdatedIntegrationEvent(isOutOfStock),
            cancellationToken);

        return Result.Success();
    }
}
