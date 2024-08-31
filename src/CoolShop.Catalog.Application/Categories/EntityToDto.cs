using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Categories;

public static class EntityToDto
{
    public static CategoryDto ToDto(this Category category)
    {
        return new(category.Id, category.Name);
    }

    public static List<CategoryDto> ToDtos(this List<Category> categories)
    {
        return categories.Select(ToDto).ToList();
    }
}
