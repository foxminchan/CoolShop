namespace CoolShop.Promotion.Features.Delete;

public sealed record DeleteCouponCommand(ObjectId Id) : ICommand<Result>;

public sealed class DeleteCouponHandler(IPromotionRepository repository)
    : ICommandHandler<DeleteCouponCommand, Result>
{
    public async Task<Result> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Coupon>.Filter.Eq(x => x.Id, request.Id);
        var coupon = await repository.GetAsync(filter, cancellationToken);

        Guard.Against.NotFound(request.Id, coupon);

        coupon.Delete();

        await repository.UpdateAsync(filter, coupon, cancellationToken);

        return Result.Success();
    }
}
