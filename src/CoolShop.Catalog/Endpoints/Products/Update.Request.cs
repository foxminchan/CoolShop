namespace CoolShop.Catalog.Endpoints.Products;

public sealed record UpdateProductRequest(
    Guid Id,
    string Name,
    string? Description,
    IFormFile? Image,
    decimal Price,
    decimal PriceSale,
    Guid CategoryId,
    Guid BrandId,
    Guid InventoryId);
