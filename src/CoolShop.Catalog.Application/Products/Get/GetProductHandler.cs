using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.Get;

public sealed class GetProductHandler(IReadRepository<Product> repository)
    : IQueryHandler<GetProductQuery, Result<ProductDto?>>
{
    public async Task<Result<ProductDto?>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        return product?.ToDto();
    }
}
