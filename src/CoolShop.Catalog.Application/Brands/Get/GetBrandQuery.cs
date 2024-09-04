namespace CoolShop.Catalog.Application.Brands.Get;

public sealed record GetBrandQuery(Guid Id) : IQuery<Result<BrandDto>>;
