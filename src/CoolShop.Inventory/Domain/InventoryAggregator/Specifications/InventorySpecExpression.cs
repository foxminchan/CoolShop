namespace CoolShop.Inventory.Domain.InventoryAggregator.Specifications;

public static class InventorySpecExpression
{
    public static ISpecificationBuilder<Inventory> ApplyOrdering(this ISpecificationBuilder<Inventory> builder,
        string? orderBy, bool isDescending)
    {
        return orderBy switch
        {
            nameof(Inventory.QuantityOnHold) => isDescending
                ? builder.OrderByDescending(x => x.QuantityOnHold)
                : builder.OrderBy(x => x.QuantityOnHold),
            nameof(Inventory.QuantityAvailable) => isDescending
                ? builder.OrderByDescending(x => x.QuantityAvailable)
                : builder.OrderBy(x => x.QuantityAvailable),
            _ => isDescending
                ? builder.OrderByDescending(x => x.Id)
                : builder.OrderBy(x => x.Id)
        };
    }

    public static ISpecificationBuilder<Inventory> ApplyPaging(this ISpecificationBuilder<Inventory> builder,
        int pageIndex, int pageSize)
    {
        return builder
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
