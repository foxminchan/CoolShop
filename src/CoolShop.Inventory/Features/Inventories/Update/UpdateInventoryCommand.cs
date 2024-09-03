using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.Update;

public sealed record UpdateInventoryCommand(Guid Id, int QuantityOnHold, Guid? SupplierId, Guid? WarehouseId)
    : ICommand<Result>;

public sealed class UpdateInventoryHandler(IRepository<InventoryModel> repository)
    : ICommandHandler<UpdateInventoryCommand, Result>
{
    public async Task<Result> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, inventory);

        inventory.Update(request.QuantityOnHold, request.SupplierId, request.WarehouseId);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
