using CoolShop.Constants;
using FluentValidation;

namespace CoolShop.Catalog.Application.Brands.Create;

public sealed class CreateBrandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);
    }
}
