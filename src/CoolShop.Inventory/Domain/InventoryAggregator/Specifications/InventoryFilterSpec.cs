namespace CoolShop.Inventory.Domain.InventoryAggregator.Specifications;

public sealed class InventoryFilterSpec : Specification<Inventory>
{
    public InventoryFilterSpec(int pageIndex, int pageSize, string? orderBy, bool isDescending)
    {
        Query
            .ApplyOrdering(orderBy, isDescending)
            .ApplyPaging(pageIndex, pageSize);
    }
}
