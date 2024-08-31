using CoolShop.Catalog.Application.Brands;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed record ListBrandsResponse(List<BrandDto> Brands);
