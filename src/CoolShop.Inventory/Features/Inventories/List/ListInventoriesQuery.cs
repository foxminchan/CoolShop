using CoolShop.Inventory.Domain.InventoryAggregator.Specifications;
using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.List;

public sealed record ListInventoriesQuery(int PageIndex, int PageSize, string? SortBy, bool IsDescending)
    : IQuery<PagedResult<IEnumerable<InventoryModel>>>;

public sealed class ListInventoriesHandler(IReadRepository<InventoryModel> repository)
    : IQueryHandler<ListInventoriesQuery, PagedResult<IEnumerable<InventoryModel>>>
{
    public async Task<PagedResult<IEnumerable<InventoryModel>>> Handle(ListInventoriesQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new InventoryFilterSpec(request.PageIndex, request.PageSize, request.SortBy, request.IsDescending);

        var result = await repository.ListAsync(spec, cancellationToken);

        var totalRecords = await repository.CountAsync(spec, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);

        PagedInfo pagedInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, result);
    }
}
