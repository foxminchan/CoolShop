namespace CoolShop.Promotion.Features.Create;

public sealed record CreateCouponCommand(
    decimal Discount,
    DateOnly ValidFrom,
    DateOnly ValidTo,
    string Code,
    Guid[]? ProductIds) : ICommand<Result<ObjectId>>;

public sealed class CreateCouponHandler(IPromotionRepository repository)
    : ICommandHandler<CreateCouponCommand, Result<ObjectId>>
{
    public async Task<Result<ObjectId>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = new Coupon(request.Discount, request.ValidFrom, request.ValidTo, request.Code, request.ProductIds);

        await repository.AddAsync(coupon, cancellationToken);

        return coupon.Id;
    }
}
