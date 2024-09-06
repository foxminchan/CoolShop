namespace CoolShop.Ordering.Features.Orders.Cancel;

public sealed record CancelOrderCommand(Guid Id) : ICommand<Result>;

public sealed class CancelOrderCommandHandler(DaprWorkflowClient daprWorkflowClient)
    : ICommandHandler<CancelOrderCommand, Result>
{
    public async Task<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(CancelOrderWorkflow),
            input: new CancelOrderWorkflowRequest(command.Id),
            instanceId: Guid.NewGuid().ToString());

        return Result.Success();
    }
}
