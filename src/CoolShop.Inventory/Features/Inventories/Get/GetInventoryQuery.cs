using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.Get;

public sealed record GetInventoryQuery(Guid Id) : IQuery<Result<InventoryModel>>;

public sealed class GetInventoryQueryHandler(IReadRepository<InventoryModel> repository)
    : IQueryHandler<GetInventoryQuery, Result<InventoryModel>>
{
    public async Task<Result<InventoryModel>> Handle(GetInventoryQuery query, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(query.Id, cancellationToken);

        if (inventory is null)
        {
            return Result.NotFound();
        }

        return inventory;
    }
}
