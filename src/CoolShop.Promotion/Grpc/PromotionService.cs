using System.Diagnostics.CodeAnalysis;
using CoolShop.Promotion.Features;
using CoolShop.Promotion.Features.Get;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CoolShop.Promotion.Grpc;

public sealed class PromotionService(ISender sender, ILogger<PromotionService> logger)
    : AppCallback.AppCallbackBase
{
    public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("[{Service}] - Invoking method: {Method}", nameof(PromotionService), request.Method);
        }

        var response = new InvokeResponse();

        switch (request.Method)
        {
            case nameof(Coupon.CouponBase.GetCoupon):
            {
                var input = request.Data.Unpack<GetCouponRequest>();

                var coupon = await RetrieveCoupon(ObjectId.Parse(input.PromotionId));

                response.Data = Any.Pack(MapToCouponResponse(coupon));

                return response;
            }
            default:
                logger.LogWarning("[{Service}] - Method not found: {Method}", nameof(PromotionService), request.Method);
                break;
        }

        return response;
    }

    public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request,
        ServerCallContext context)
    {
        return Task.FromResult(new ListTopicSubscriptionsResponse());
    }

    private async Task<CouponDto> RetrieveCoupon(ObjectId id)
    {
        var coupon = await sender.Send(new GetCouponQuery(id));

        if (coupon.Value is null)
        {
            ThrowNotFound();
        }

        return coupon.Value;
    }

    [DoesNotReturn]
    private static void ThrowNotFound()
    {
        throw new RpcException(new(StatusCode.NotFound, "Coupon not found"));
    }

    private static GetCouponResponse MapToCouponResponse(CouponDto coupon)
    {
        return new()
        {
            Id = coupon.Id.ToString(),
            Code = coupon.Code,
            Discount = decimal.ToDouble(coupon.Discount),
            ValidaTo =
                new() { Day = coupon.ValidTo.Year, Month = coupon.ValidTo.Month, Year = coupon.ValidTo.Year },
            ValidFrom = new()
            {
                Day = coupon.ValidFrom.Year, Month = coupon.ValidFrom.Month, Year = coupon.ValidFrom.Year
            }
        };
    }
}
