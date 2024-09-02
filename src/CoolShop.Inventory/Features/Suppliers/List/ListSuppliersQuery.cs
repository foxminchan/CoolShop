using CoolShop.Inventory.Domain.SupplierAggregator;
using CoolShop.Inventory.Domain.SupplierAggregator.Specifications;

namespace CoolShop.Inventory.Features.Suppliers.List;

public sealed record ListSuppliersQuery(int PageIndex, int PageSize, string? SearchQuery)
    : IQuery<PagedResult<IEnumerable<Supplier>>>;

public sealed class ListSuppliersHandler(IReadRepository<Supplier> repository)
    : IQueryHandler<ListSuppliersQuery, PagedResult<IEnumerable<Supplier>>>
{
    public async Task<PagedResult<IEnumerable<Supplier>>> Handle(ListSuppliersQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new SupplierFilterSpec(request.PageIndex, request.PageSize, request.SearchQuery);

        var result = await repository.ListAsync(spec, cancellationToken);

        var totalRecords = await repository.CountAsync(spec, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);

        PagedInfo pagedInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, result);
    }
}
