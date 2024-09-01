namespace CoolShop.Catalog.Application.Brands.List;

public sealed record ListBrandsQuery : IQuery<Result<IEnumerable<BrandDto>>>;
