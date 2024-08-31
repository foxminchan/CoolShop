using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed record ListProductsRequest(
    int PageIndex,
    int PageSize,
    string? OrderBy,
    bool IsDescending,
    Status[]? Statuses,
    Guid?[]? CategoryId,
    Guid?[]? BrandId);
