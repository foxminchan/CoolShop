using CoolShop.Catalog.Application.Categories;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed record ListCategoriesResponse(List<CategoryDto> Categories);
