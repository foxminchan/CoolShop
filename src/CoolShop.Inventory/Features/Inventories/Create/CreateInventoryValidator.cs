namespace CoolShop.Inventory.Features.Inventories.Create;

internal sealed class CreateInventoryValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryValidator()
    {
        RuleFor(x => x.QuantityAvailable)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.QuantityOnHold)
            .GreaterThanOrEqualTo(x => x.QuantityAvailable);
    }
}
