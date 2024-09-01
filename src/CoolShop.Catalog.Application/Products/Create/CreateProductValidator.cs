using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.Create;

internal sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.Description)
            .MaximumLength(DataSchemaLength.SuperLarge);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceSale)
            .LessThanOrEqualTo(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceSale > 0);

        RuleFor(x => x.Status)
            .Must(x => x is Status.ComingSoon or Status.InStock);

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
