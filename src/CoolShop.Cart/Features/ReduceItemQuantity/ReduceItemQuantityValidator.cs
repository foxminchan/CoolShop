using FluentValidation;

namespace CoolShop.Cart.Features.ReduceItemQuantity;

internal sealed class ReduceItemQuantityValidator : AbstractValidator<ReduceItemQuantityCommand>
{
    public ReduceItemQuantityValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}
