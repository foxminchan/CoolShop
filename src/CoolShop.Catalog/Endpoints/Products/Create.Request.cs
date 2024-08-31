using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed record CreateProductRequest(
    string Name,
    string? Description,
    IFormFile? Image,
    decimal Price,
    decimal PriceSale,
    Status Status,
    Guid CategoryId,
    Guid BrandId,
    Guid InventoryId);
