using CoolShop.Catalog.Grpc;

namespace CoolShop.Promotion.Features.Create;

internal sealed class CreateCouponValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponValidator(ProductValidator productValidator)
    {
        RuleFor(x => x.Discount)
            .NotEmpty()
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(100);

        RuleFor(x => x.ValidFrom)
            .NotEmpty()
            .LessThan(x => x.ValidTo);

        RuleFor(x => x.ValidTo)
            .NotEmpty()
            .GreaterThan(x => x.ValidFrom);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(DataSchemaLength.Tiny);

        RuleFor(x => x.ProductIds)
            .SetValidator(productValidator);
    }
}

internal sealed class ProductValidator : AbstractValidator<Guid[]?>
{
    private readonly DaprClient _daprClient;

    public ProductValidator(DaprClient daprClient)
    {
        _daprClient = daprClient;

        RuleFor(x => x)
            .MustAsync(Exist);
    }

    private async Task<bool> Exist(Guid[]? productId, CancellationToken cancellationToken)
    {
        if (productId is null || !productId.Any())
        {
            return true;
        }

        var ids = productId.Select(x => x.ToString()).ToList();

        var product = await _daprClient.InvokeMethodAsync<ListProductStatusRequest, ListProductStatusResponse>(
            ServiceName.AppId.Catalog,
            nameof(Product.ProductClient.GetListProductStatus),
            new() { Ids = { ids } },
            cancellationToken);

        return product.Statuses.Select(x => x.Value)
            .All(x => x);
    }
}
