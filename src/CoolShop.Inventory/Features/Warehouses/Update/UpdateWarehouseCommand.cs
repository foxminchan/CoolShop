using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Update;

public sealed record UpdateWarehouseCommand(Guid Id, string? Name, string? Location, int Capacity) : ICommand<Result>;

public sealed class UpdateWarehouseHandler(IRepository<Warehouse> repository)
    : ICommandHandler<UpdateWarehouseCommand, Result>
{
    public async Task<Result> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, warehouse);

        warehouse.Update(request.Name, request.Location, request.Capacity);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
