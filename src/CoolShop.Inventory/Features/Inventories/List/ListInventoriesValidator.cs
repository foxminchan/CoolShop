namespace CoolShop.Inventory.Features.Inventories.List;

public sealed class ListInventoriesValidator : AbstractValidator<ListInventoriesQuery>
{
    public ListInventoriesValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}
