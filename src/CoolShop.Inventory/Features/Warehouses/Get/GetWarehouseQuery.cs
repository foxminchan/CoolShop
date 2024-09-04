using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Get;

public sealed record GetWarehouseQuery(Guid Id) : IQuery<Result<Warehouse>>;

public sealed class GetWarehouseHandler(IReadRepository<Warehouse> repository)
    : IQueryHandler<GetWarehouseQuery, Result<Warehouse>>
{
    public async Task<Result<Warehouse>> Handle(GetWarehouseQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(query.Id, cancellationToken);

        if (warehouse is null)
        {
            return Result.NotFound();
        }

        return warehouse;
    }
}
