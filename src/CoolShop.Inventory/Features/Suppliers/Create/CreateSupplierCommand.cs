using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Create;

public sealed record CreateSupplierCommand(string? Name, string? PhoneNumber) : ICommand<Result<Guid>>;

public sealed class CreateSupplierHandler(IRepository<Supplier> repository)
    : ICommandHandler<CreateSupplierCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.Name, request.PhoneNumber);

        var result = await repository.AddAsync(supplier, cancellationToken);

        return result.Id;
    }
}
