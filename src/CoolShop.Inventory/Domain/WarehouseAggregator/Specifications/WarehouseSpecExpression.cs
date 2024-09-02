namespace CoolShop.Inventory.Domain.WarehouseAggregator.Specifications;

public static class WarehouseSpecExpression
{
    public static ISpecificationBuilder<Warehouse> ApplyOrdering(this ISpecificationBuilder<Warehouse> builder,
        string? orderBy, bool isDescending)
    {
        return orderBy switch
        {
            nameof(Warehouse.Name) => isDescending
                ? builder.OrderByDescending(x => x.Name)
                : builder.OrderBy(x => x.Name),
            nameof(Warehouse.Location) => isDescending
                ? builder.OrderByDescending(x => x.Location)
                : builder.OrderBy(x => x.Location),
            nameof(Warehouse.Capacity) => isDescending
                ? builder.OrderByDescending(x => x.Capacity)
                : builder.OrderBy(x => x.Capacity),
            _ => isDescending
                ? builder.OrderByDescending(x => x.Capacity)
                : builder.OrderBy(x => x.Capacity)
        };
    }

    public static ISpecificationBuilder<Warehouse> ApplyPaging(this ISpecificationBuilder<Warehouse> builder,
        int pageIndex, int pageSize)
    {
        return builder
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
