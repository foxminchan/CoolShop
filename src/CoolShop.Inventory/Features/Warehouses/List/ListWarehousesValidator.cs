namespace CoolShop.Inventory.Features.Warehouses.List;

internal sealed class ListWarehousesValidator : AbstractValidator<ListWarehousesQuery>
{
    public ListWarehousesValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .MaximumLength(DataSchemaLength.Large);
    }
}
