using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Create;

public sealed record CreateWarehouseCommand(string? Name, string? Location, int Capacity) : ICommand<Result<Guid>>;

public sealed class CreateWarehouseHandler(IRepository<Warehouse> repository)
    : ICommandHandler<CreateWarehouseCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse(request.Name, request.Location, request.Capacity);

        await repository.AddAsync(warehouse, cancellationToken);

        return warehouse.Id;
    }
}
