using CoolShop.Catalog.Grpc;
using CoolShop.Promotion.Grpc;

namespace CoolShop.Cart.Features.Create;

internal sealed class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator(ProductValidator productValidator, CouponValidator couponValidator)
    {
        RuleFor(x => x.ProductId)
            .SetValidator(productValidator);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.CouponId)
            .SetValidator(couponValidator);
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

internal sealed class CouponValidator : AbstractValidator<Guid?>
{
    private readonly DaprClient _daprClient;

    public CouponValidator(DaprClient daprClient)
    {
        _daprClient = daprClient;

        RuleFor(x => x)
            .MustAsync(Exist)
            .WithMessage("Coupon does not exist")
            .MustAsync(NotExpired)
            .WithMessage("Coupon is expired");
    }

    private async Task<bool> Exist(Guid? couponId, CancellationToken cancellationToken)
    {
        if (couponId is null)
        {
            return true;
        }

        var coupon = await GetCouponAsync(couponId.Value, cancellationToken);

        return coupon is not null;
    }

    private async Task<bool> NotExpired(Guid? couponId, CancellationToken cancellationToken)
    {
        if (couponId is null)
        {
            return true;
        }

        var coupon = await GetCouponAsync(couponId.Value, cancellationToken);

        return coupon is not null && new DateOnly(coupon.ValidaTo.Year, coupon.ValidaTo.Month, coupon.ValidaTo.Day) >=
            DateOnly.FromDateTime(DateTime.UtcNow);
    }


    private async Task<GetCouponResponse?> GetCouponAsync(Guid couponId, CancellationToken cancellationToken)
    {
        return await _daprClient.InvokeMethodAsync<GetCouponRequest, GetCouponResponse>(
            ServiceName.AppId.Promotion,
            nameof(Coupon.CouponClient.GetCoupon),
            new() { PromotionId = couponId.ToString() },
            cancellationToken);
    }
}
