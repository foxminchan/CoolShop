using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Activities;

public sealed class CancelOrderActivity(IRepository<Order> repository, ILoggerFactory loggerFactory)
    : WorkflowActivity<Guid, object?>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CancelOrderActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Guid input)
    {
        _logger.LogInformation("[{Activity}] - Canceling order with id {OrderId}", nameof(CancelOrderActivity), input);

        var order = await repository.GetByIdAsync(input);

        if (order is null)
        {
            _logger.LogWarning("[{Activity}] - Order with id {OrderId} not found", nameof(CancelOrderActivity), input);
            return null;
        }

        order.MarkAsCancelled();

        await repository.SaveChangesAsync();

        OrderingTraceExtension.LogOrderCancelled(_logger, nameof(CancelOrderActivity), input);

        return null;
    }
}
