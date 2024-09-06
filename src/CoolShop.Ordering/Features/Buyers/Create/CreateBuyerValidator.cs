namespace CoolShop.Ordering.Features.Buyers.Create;

internal sealed class CreateBuyerValidator : AbstractValidator<CreateBuyerCommand>
{
    public CreateBuyerValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.Province)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Tiny);

        RuleFor(x => x.Email)
            .MaximumLength(DataSchemaLength.Medium)
            .EmailAddress();
    }
}
