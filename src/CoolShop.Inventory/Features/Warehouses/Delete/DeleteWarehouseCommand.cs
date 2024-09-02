using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Delete;

public sealed record DeleteWarehouseCommand(Guid Id) : ICommand<Result>;

public sealed class DeleteWarehouseHandler(IRepository<Warehouse> repository)
    : ICommandHandler<DeleteWarehouseCommand, Result>
{
    public async Task<Result> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, warehouse);

        await repository.DeleteAsync(warehouse, cancellationToken);

        return Result.Success();
    }
}
