using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Update;

public sealed record UpdateSupplierCommand(Guid Id, string? Name, string? PhoneNumber) : ICommand<Result>;

public sealed class UpdateSupplierHandler(IRepository<Supplier> repository)
    : ICommandHandler<UpdateSupplierCommand, Result>
{
    public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, supplier);

        supplier.Update(request.Name, request.PhoneNumber);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
