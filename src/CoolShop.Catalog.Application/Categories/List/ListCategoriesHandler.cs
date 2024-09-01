using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Categories.List;

public sealed class ListCategoriesHandler(IReadRepository<Category> repository)
    : IQueryHandler<ListCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    public async Task<Result<IEnumerable<CategoryDto>>> Handle(ListCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await repository.ListAsync(cancellationToken);

        return categories.ToDtos();
    }
}
