using CoolShop.Ordering.Domain.OrderAggregate;
using CoolShop.Ordering.Domain.OrderAggregate.Specifications;

namespace CoolShop.Ordering.Features.Orders.Ship;

public sealed record ShipOrderCommand(Guid Id) : ICommand<Result>;

public sealed class ShipOrderHandler(IRepository<Order> repository, ILogger<ShipOrderHandler> logger)
    : ICommandHandler<ShipOrderCommand, Result>
{
    public async Task<Result> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.FirstOrDefaultAsync(new OrderFilterSpec(request.Id), cancellationToken);

        Guard.Against.NotFound(request.Id, order);

        order.MarkAsShipped();

        await repository.SaveChangesAsync(cancellationToken);

        OrderingTraceExtension.LogOrderShipped(logger, "OrderShipped", order.Id);

        return Result.Success();
    }
}
