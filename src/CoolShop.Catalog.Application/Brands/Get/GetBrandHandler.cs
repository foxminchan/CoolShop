using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Brands.Get;

public sealed class GetBrandHandler(IReadRepository<Brand> repository)
    : IQueryHandler<GetBrandQuery, Result<BrandDto>>
{
    public async Task<Result<BrandDto>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (brand is null)
        {
            return Result.NotFound();
        }

        return brand.ToDto();
    }
}
