using CoolShop.Constants;
using FluentValidation;

namespace CoolShop.Catalog.Application.Products.Update;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.Description)
            .MaximumLength(DataSchemaLength.SuperLarge);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceSale)
            .LessThan(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceSale > 0);
    }
}
