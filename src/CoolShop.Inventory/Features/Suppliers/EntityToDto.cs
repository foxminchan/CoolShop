using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers;

public static class EntityToDto
{
    public static SupplierDto ToDto(this Supplier supplier)
    {
        return new(supplier.Id, supplier.Name, supplier.PhoneNumber);
    }

    public static IEnumerable<SupplierDto> ToDtos(this IEnumerable<Supplier> suppliers)
    {
        return suppliers.Select(ToDto);
    }
}
