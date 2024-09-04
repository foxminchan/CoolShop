namespace CoolShop.Catalog.Application.Categories.Get;

public sealed record GetCategoryQuery(Guid Id) : IQuery<Result<CategoryDto>>;
