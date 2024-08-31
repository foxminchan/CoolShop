using Ardalis.Specification;

namespace CoolShop.Catalog.Domain.ProductAggregator.Specifications;

public sealed class ProductFilterSpec : Specification<Product>
{
    public ProductFilterSpec(
        int pageIndex,
        int pageSize,
        string? orderBy,
        bool isDescending,
        Status[]? statuses,
        Guid?[]? categoryId,
        Guid?[]? brandId)
    {
        if (statuses is not null && statuses.Length > 0)
        {
            Query.Where(x => statuses.Contains(x.Status));
        }

        if (categoryId is not null && categoryId.Length > 0)
        {
            Query.Where(x => categoryId.Contains(x.CategoryId));
        }

        if (brandId is not null && brandId.Length > 0)
        {
            Query.Where(x => brandId.Contains(x.BrandId));
        }

        Query
            .Include(x => x.Category)
            .Include(x => x.Brand)
            .ApplyOrdering(orderBy, isDescending)
            .ApplyPaging(pageIndex, pageSize);
    }
}
