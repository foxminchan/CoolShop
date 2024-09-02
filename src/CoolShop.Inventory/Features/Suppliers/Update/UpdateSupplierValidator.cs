namespace CoolShop.Inventory.Features.Suppliers.Update;

internal sealed class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Large);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");
    }
}
