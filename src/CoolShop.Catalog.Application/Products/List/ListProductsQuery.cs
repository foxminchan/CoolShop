using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.List;

public sealed record ListProductsQuery(
    int PageIndex,
    int PageSize,
    string? OrderBy,
    bool IsDescending,
    Status[]? Statuses,
    Guid?[]? CategoryId,
    Guid?[]? BrandId) : IQuery<PagedResult<IEnumerable<ProductDto>>>;
