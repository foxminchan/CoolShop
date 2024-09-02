namespace CoolShop.Inventory.Features.Suppliers.Create;

internal sealed class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Large);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");
    }
}
