using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.Get;

public sealed record GetWarehouseQuery(Guid Id) : IQuery<Result<WarehouseDto?>>;

public sealed class GetWarehouseHandler(IReadRepository<Warehouse> repository)
    : IQueryHandler<GetWarehouseQuery, Result<WarehouseDto?>>
{
    public async Task<Result<WarehouseDto?>> Handle(GetWarehouseQuery query, CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(query.Id, cancellationToken);

        return warehouse?.ToDto();
    }
}
