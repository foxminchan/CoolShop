namespace CoolShop.Rating.Features.List;

internal sealed class ListFeedbackValidator : AbstractValidator<ListFeedbackQuery>
{
    public ListFeedbackValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}
