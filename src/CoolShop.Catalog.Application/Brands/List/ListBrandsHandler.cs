﻿using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Brands.List;

public sealed class ListBrandsHandler(IReadRepository<Brand> repository)
    : IQueryHandler<ListBrandsQuery, Result<IEnumerable<BrandDto>>>
{
    public async Task<Result<IEnumerable<BrandDto>>> Handle(ListBrandsQuery query, CancellationToken cancellationToken)
    {
        var brands = await repository.ListAsync(cancellationToken);

        return brands.ToDtos();
    }
}
