using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.Create;

public sealed record CreateInventoryCommand(
    int QuantityAvailable,
    int QuantityOnHold,
    Guid? SupplierId,
    Guid? WarehouseId) : ICommand<Result<Guid>>;

public sealed class CreateInventoryHandler(IRepository<InventoryModel> repository)
    : ICommandHandler<CreateInventoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = new InventoryModel(request.QuantityAvailable, request.QuantityOnHold, request.SupplierId,
            request.WarehouseId);

        var result = await repository.AddAsync(inventory, cancellationToken);

        return result.Id;
    }
}
