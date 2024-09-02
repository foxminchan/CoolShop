namespace CoolShop.Inventory.Domain.SupplierAggregator.Specifications;

public sealed class SupplierFilterSpec : Specification<Supplier>
{
    public SupplierFilterSpec(int pageIndex, int pageSize, string? searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            Query.Where(x => x.Name!.Contains(searchQuery) || x.PhoneNumber!.Contains(searchQuery));
        }

        Query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
