namespace CoolShop.Inventory.Features.Warehouses.Create;

internal sealed class CreateWarehouseValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Large);

        RuleFor(x => x.Location)
            .MaximumLength(DataSchemaLength.SuperLarge);

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(100);
    }
}
