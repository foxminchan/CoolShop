using CoolShop.Catalog.Application.Products.Workflows;

namespace CoolShop.Catalog.Application.Products.ReduceQuantity;

public sealed class ReduceQuantityHandler(DaprWorkflowClient daprWorkflowClient)
    : ICommandHandler<ReduceQuantityCommand, Result>
{
    public async Task<Result> Handle(ReduceQuantityCommand request, CancellationToken cancellationToken)
    {
        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(ReduceQuantityWorkflow),
            input: request,
            instanceId: Guid.NewGuid().ToString());

        return Result.Success();
    }
}
