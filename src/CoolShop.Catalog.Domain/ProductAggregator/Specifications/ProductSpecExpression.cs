using Ardalis.Specification;

namespace CoolShop.Catalog.Domain.ProductAggregator.Specifications;

public static class ProductSpecExpression
{
    public static ISpecificationBuilder<Product> ApplyOrdering(this ISpecificationBuilder<Product> builder,
        string? orderBy, bool isDescending)
    {
        return orderBy switch
        {
            nameof(Product.Name) => isDescending
                ? builder.OrderByDescending(x => x.Name)
                : builder.OrderBy(x => x.Name),
            nameof(Product.Price.OriginalPrice) => isDescending
                ? builder.OrderByDescending(x => x.Price!.OriginalPrice)
                : builder.OrderBy(x => x.Price!.OriginalPrice),
            nameof(Product.Price.DiscountPrice) => isDescending
                ? builder.OrderByDescending(x => x.Price!.DiscountPrice)
                : builder.OrderBy(x => x.Price!.DiscountPrice),
            nameof(Product.Status) => isDescending
                ? builder.OrderByDescending(x => x.Status)
                : builder.OrderBy(x => x.Status),
            _ => isDescending
                ? builder.OrderByDescending(x => x.Name)
                : builder.OrderBy(x => x.Name)
        };
    }

    public static ISpecificationBuilder<Product> ApplyPaging(this ISpecificationBuilder<Product> builder,
        int pageIndex, int pageSize)
    {
        return builder
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
