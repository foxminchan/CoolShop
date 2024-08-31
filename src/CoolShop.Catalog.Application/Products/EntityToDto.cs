using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products;

public static class EntityToDto
{
    public static ProductDto ToDto(this Product product)
    {
        var imageUrl = product.Image is not null ? $"/Pics/{product.Image}" : null;

        return new(
            product.Id,
            product.Name,
            product.Description,
            imageUrl,
            product.Price!.OriginalPrice,
            product.Price!.DiscountPrice ?? -1,
            product.Status,
            product.Rating,
            product.ReviewsCount,
            product.Category?.Name,
            product.Brand?.Name);
    }

    public static List<ProductDto> ToDtos(this List<Product> products)
    {
        return products.Select(ToDto).ToList();
    }
}
