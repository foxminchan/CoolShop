namespace CoolShop.Catalog.Application.Products.List;

public sealed class ListProductsValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleFor(x => x.Statuses)
            .ForEach(x => x.IsInEnum());
    }
}
