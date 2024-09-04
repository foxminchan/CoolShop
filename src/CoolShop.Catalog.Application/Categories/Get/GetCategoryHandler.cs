using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Categories.Get;

public sealed class GetCategoryHandler(IReadRepository<Category> repository)
    : IQueryHandler<GetCategoryQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (category is null)
        {
            return Result.NotFound();
        }

        return category.ToDto();
    }
}
