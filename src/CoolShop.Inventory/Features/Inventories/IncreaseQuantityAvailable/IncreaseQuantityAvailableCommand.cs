using CoolShop.Inventory.IntegrationEvents;
using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.IncreaseQuantityAvailable;

public sealed record IncreaseQuantityAvailableCommand(Dictionary<Guid, int> InventoryQuantities)
    : ICommand<Result>;

public sealed class IncreaseQuantityAvailableHandler(IRepository<InventoryModel> repository, DaprClient daprClient)
    : ICommandHandler<IncreaseQuantityAvailableCommand, Result>
{
    public async Task<Result> Handle(IncreaseQuantityAvailableCommand request, CancellationToken cancellationToken)
    {
        var inventories = await repository.ListAsync(cancellationToken);

        inventories.ForEach(inventory =>
        {
            if (request.InventoryQuantities.TryGetValue(inventory.Id, out var quantity))
            {
                inventory.IncreaseQuantityAvailable(quantity);
            }
        });

        var isInOfStock = inventories
            .Where(inventory => inventory.QuantityAvailable > 0)
            .ToDictionary(inventory => inventory.Id, _ => true);

        await repository.SaveChangesAsync(cancellationToken);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(InventoryUpdatedIntegrationEvent).ToLowerInvariant(),
            new InventoryUpdatedIntegrationEvent(isInOfStock),
            cancellationToken);

        return Result.Success();
    }
}
