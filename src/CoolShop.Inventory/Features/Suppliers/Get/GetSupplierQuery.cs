using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Get;

public sealed record GetSupplierQuery(Guid Id) : IQuery<Result<SupplierDto?>>;

public sealed class GetSupplierHandler(IReadRepository<Supplier> repository)
    : IQueryHandler<GetSupplierQuery, Result<SupplierDto?>>
{
    public async Task<Result<SupplierDto?>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        var supplier = await repository.GetByIdAsync(request.Id, cancellationToken);

        return supplier?.ToDto();
    }
}
