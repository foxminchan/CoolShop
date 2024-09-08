using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.GetStatusRange;

public sealed class GetStatusRangeHandler(IReadRepository<Product> readRepository)
    : IQueryHandler<GetStatusRangeQuery, Result<Dictionary<Guid, bool>>>
{
    public async Task<Result<Dictionary<Guid, bool>>> Handle(GetStatusRangeQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<Guid, bool> result = new();

        foreach (var id in request.Ids)
        {
            var product = await readRepository.GetByIdAsync(id, cancellationToken);

            result.Add(id, product is not null);
        }

        return result;
    }
}
