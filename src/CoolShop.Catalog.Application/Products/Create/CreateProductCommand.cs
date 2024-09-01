using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    IFormFile? Image,
    decimal Price,
    decimal PriceSale,
    Status Status,
    Guid CategoryId,
    Guid BrandId,
    Guid InventoryId) : ICommand<Result<Guid>>;
