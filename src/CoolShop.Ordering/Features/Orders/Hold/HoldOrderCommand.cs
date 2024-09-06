using CoolShop.Ordering.Domain.OrderAggregate;
using CoolShop.Ordering.Domain.OrderAggregate.Specifications;

namespace CoolShop.Ordering.Features.Orders.Hold;

public sealed record HoldOrderCommand(Guid Id) : ICommand<Result>;

public sealed class HoldOrderHandler(IRepository<Order> repository, ILogger<HoldOrderHandler> logger)
    : ICommandHandler<HoldOrderCommand, Result>
{
    public async Task<Result> Handle(HoldOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.FirstOrDefaultAsync(new OrderFilterSpec(request.Id), cancellationToken);

        Guard.Against.NotFound(request.Id, order);

        order.MarkAsHeld();

        await repository.SaveChangesAsync(cancellationToken);

        OrderingTraceExtension.LogOrderHeld(logger, "OrderHeld", order.Id);

        return Result.Success();
    }
}
