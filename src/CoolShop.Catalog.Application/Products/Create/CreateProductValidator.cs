using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Inventory.Grpc;

namespace CoolShop.Catalog.Application.Products.Create;

public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator(InventoryValidator inventoryValidator)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaLength.Medium);

        RuleFor(x => x.Description)
            .MaximumLength(DataSchemaLength.SuperLarge);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceSale)
            .LessThanOrEqualTo(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PriceSale > 0);

        RuleFor(x => x.Status)
            .Must(x => x is Status.ComingSoon or Status.InStock);

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.BrandId)
            .NotEmpty();

        RuleFor(x => x.InventoryId)
            .SetValidator(inventoryValidator);
    }
}

public sealed class InventoryValidator : AbstractValidator<Guid>
{
    private readonly DaprClient _daprClient;

    public InventoryValidator(DaprClient daprClient)
    {
        _daprClient = daprClient;

        RuleFor(x => x)
            .NotEmpty()
            .MustAsync(Exist)
            .WithMessage("Inventory does not exist");
    }

    private async Task<bool> Exist(Guid inventoryId, CancellationToken cancellationToken)
    {
        var inventory = await _daprClient.InvokeMethodAsync<InventoryRequest, InventoryResponse>(
            ServiceName.AppId.Inventory,
            nameof(Inventory.Grpc.Inventory.InventoryClient.GetInventory),
            new() { InventoryId = inventoryId.ToString() },
            cancellationToken);

        return inventory is not null;
    }
}
