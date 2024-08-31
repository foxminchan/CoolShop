using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products;

public sealed record ProductDto(
    Guid Id,
    string? Name,
    string? Description,
    string? ImageUrl,
    decimal Price,
    decimal PriceSale,
    Status Status,
    double Rating,
    long ReviewsCount,
    string? Category,
    string? Brand);
