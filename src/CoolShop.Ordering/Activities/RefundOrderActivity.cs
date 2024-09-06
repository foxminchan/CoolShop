using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Activities;

public sealed class RefundOrderActivity(IRepository<Order> repository, ILoggerFactory loggerFactory)
    : WorkflowActivity<Guid, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RefundOrderActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Guid input)
    {
        _logger.LogInformation("[{Activity}] - Refunding order with id {OrderId}", nameof(RefundOrderActivity), input);

        var order = await repository.GetByIdAsync(input);

        if (order is null)
        {
            _logger.LogWarning("[{Activity}] - Order with id {OrderId} not found", nameof(RefundOrderActivity), input);
            return null;
        }

        order.MarkAsRefund();

        await repository.SaveChangesAsync();

        OrderingTraceExtension.LogOrderCancelled(_logger, nameof(RefundOrderActivity), input);

        return null;
    }
}
