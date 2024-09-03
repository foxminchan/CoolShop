using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Get;

public sealed record GetSupplierQuery(Guid Id) : IQuery<Result<Supplier?>>;

public sealed class GetSupplierHandler(IReadRepository<Supplier> repository)
    : IQueryHandler<GetSupplierQuery, Result<Supplier?>>
{
    public async Task<Result<Supplier?>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var supplier = await repository.GetByIdAsync(request.Id, cancellationToken);

        return supplier;
    }
}
