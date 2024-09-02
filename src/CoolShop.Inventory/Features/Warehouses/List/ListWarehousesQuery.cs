using CoolShop.Inventory.Domain.WarehouseAggregator;
using CoolShop.Inventory.Domain.WarehouseAggregator.Specifications;

namespace CoolShop.Inventory.Features.Warehouses.List;

public sealed record ListWarehousesQuery(int PageIndex, int PageSize, string? Name, string? OrderBy, bool IsDescending)
    : IQuery<PagedResult<IEnumerable<Warehouse>>>;

public sealed class ListWarehousesHandler(IReadRepository<Warehouse> repository)
    : IQueryHandler<ListWarehousesQuery, PagedResult<IEnumerable<Warehouse>>>
{
    public async Task<PagedResult<IEnumerable<Warehouse>>> Handle(ListWarehousesQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new WarehouseFilterSpec(request.PageIndex, request.PageSize, request.Name, request.OrderBy,
            request.IsDescending);

        var result = await repository.ListAsync(spec, cancellationToken);

        var totalRecords = await repository.CountAsync(spec, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);

        PagedInfo pagedInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, result);
    }
}
