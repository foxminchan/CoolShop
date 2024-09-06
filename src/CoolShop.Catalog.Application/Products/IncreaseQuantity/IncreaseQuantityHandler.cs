using CoolShop.Catalog.Application.Products.Workflows;

namespace CoolShop.Catalog.Application.Products.IncreaseQuantity;

public sealed class IncreaseQuantityHandler(DaprWorkflowClient daprWorkflowClient)
    : ICommandHandler<IncreaseQuantityCommand, Result>
{
    public async Task<Result> Handle(IncreaseQuantityCommand request, CancellationToken cancellationToken)
    {
        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(IncreaseQuantityWorkflow),
            input: request,
            instanceId: Guid.NewGuid().ToString());

        return Result.Success();
    }
}
