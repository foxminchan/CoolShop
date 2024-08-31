using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Brands;

public static class EntityToDto
{
    public static BrandDto ToDto(this Brand brand)
    {
        return new(brand.Id, brand.Name);
    }

    public static List<BrandDto> ToDtos(this List<Brand> brands)
    {
        return brands.Select(ToDto).ToList();
    }
}
