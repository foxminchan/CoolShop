using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Domain.ProductAggregator.Specifications;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.List;

public sealed class ListProductsHandler(IReadRepository<Product> repository)
    : IQueryHandler<ListProductsQuery, PagedResult<IEnumerable<ProductDto>>>
{
    public async Task<PagedResult<IEnumerable<ProductDto>>> Handle(ListProductsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ProductFilterSpec(
            request.PageIndex,
            request.PageSize,
            request.OrderBy,
            request.IsDescending,
            request.Statuses,
            request.CategoryId,
            request.BrandId);

        var products = await repository.ListAsync(spec, cancellationToken);

        var totalRecords = await repository.CountAsync(spec, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);

        PagedInfo pagedInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, products.ToDtos());
    }
}
