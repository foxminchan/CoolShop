using CoolShop.Catalog.Grpc;

namespace CoolShop.Cart.Features.Create;

internal sealed class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator(ProductValidator productValidator)
    {
        RuleFor(x => x.ProductId)
            .SetValidator(productValidator);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}

internal sealed class ProductValidator : AbstractValidator<Guid>
{
    private readonly DaprClient _daprClient;

    public ProductValidator(DaprClient daprClient)
    {
        _daprClient = daprClient;

        RuleFor(x => x)
            .NotEmpty()
            .MustAsync(Exist)
            .WithMessage("Product does not exist")
            .MustAsync(InStock)
            .WithMessage("Product is not in stock");
    }


    private async Task<bool> Exist(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _daprClient.InvokeMethodAsync<ProductRequest, ProductResponse>(
            ServiceName.AppId.Catalog,
            nameof(Product.ProductClient.GetProduct),
            new() { Id = productId.ToString() },
            cancellationToken);

        return product is not null;
    }

    private async Task<bool> InStock(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _daprClient.InvokeMethodAsync<ProductStatusRequest, ProductStatusResponse>(
            ServiceName.AppId.Catalog,
            nameof(Product.ProductClient.GetProductStatus),
            new() { Id = productId.ToString() },
            cancellationToken);

        return product.Status == nameof(InStock);
    }
}
