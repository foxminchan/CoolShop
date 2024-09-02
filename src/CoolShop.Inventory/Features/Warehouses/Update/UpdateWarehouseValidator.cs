namespace CoolShop.Inventory.Features.Warehouses.Update;

internal sealed class UpdateWarehouseValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Large);

        RuleFor(x => x.Location)
            .MaximumLength(DataSchemaLength.SuperLarge);

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(100);
    }
}
