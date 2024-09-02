namespace CoolShop.Inventory.Features.Suppliers.List;

internal sealed class ListSuppliersValidator : AbstractValidator<ListSuppliersQuery>
{
    public ListSuppliersValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}
