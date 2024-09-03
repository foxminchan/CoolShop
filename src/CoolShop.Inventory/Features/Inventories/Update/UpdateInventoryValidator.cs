using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Features.Inventories.Update;

internal sealed class UpdateInventoryValidator : AbstractValidator<UpdateInventoryCommand>
{
    private readonly IReadRepository<InventoryModel> _repository;

    public UpdateInventoryValidator(IReadRepository<InventoryModel> repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.QuantityOnHold)
            .GreaterThanOrEqualTo(0)
            .MustAsync(GreaterThanOrEqualToQuantityAvailable)
            .WithMessage("Quantity on hold must be greater than or equal to quantity available");
    }

    private async Task<bool> GreaterThanOrEqualToQuantityAvailable(UpdateInventoryCommand command, int quantityOnHold,
        CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByIdAsync(command.Id, cancellationToken);

        Guard.Against.NotFound(command.Id, inventory);

        return inventory.QuantityAvailable >= quantityOnHold;
    }
}
