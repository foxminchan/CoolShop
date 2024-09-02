namespace CoolShop.Inventory.Domain.WarehouseAggregator.Specifications;

public sealed class WarehouseFilterSpec : Specification<Warehouse>
{
    public WarehouseFilterSpec(int pageIndex, int pageSize, string? name, string? orderBy, bool isDescending)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Query.Where(x => x.Name!.Contains(name));
        }

        Query
            .ApplyOrdering(orderBy, isDescending)
            .ApplyPaging(pageIndex, pageSize);
    }
}
