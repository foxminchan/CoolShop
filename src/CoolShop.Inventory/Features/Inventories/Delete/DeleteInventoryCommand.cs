using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.Delete;

public sealed record DeleteInventoryCommand(Guid Id) : ICommand<Result>;

public sealed class DeleteInventoryHandler(IRepository<InventoryModel> repository)
    : ICommandHandler<DeleteInventoryCommand, Result>
{
    public async Task<Result> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, inventory);

        inventory.Delete();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
