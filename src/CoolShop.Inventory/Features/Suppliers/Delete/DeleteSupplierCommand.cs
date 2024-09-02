using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Delete;

public sealed record DeleteSupplierCommand(Guid Id) : ICommand<Result>;

public sealed class DeleteSupplierHandler(IRepository<Supplier> repository)
    : ICommandHandler<DeleteSupplierCommand, Result>
{
    public async Task<Result> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken = default)
    {
        var supplier = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, supplier);

        await repository.DeleteAsync(supplier, cancellationToken);

        return Result.Success();
    }
}
