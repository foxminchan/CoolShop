using CoolShop.Constants;
using FluentValidation;

namespace CoolShop.Catalog.Application.Categories.Create;

public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);
    }
}
