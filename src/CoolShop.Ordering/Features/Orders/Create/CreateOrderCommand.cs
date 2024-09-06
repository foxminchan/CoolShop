using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.Create;

public sealed record CreateOrderCommand(string? Note, PaymentMethod PaymentMethod) : ICommand<Result<Guid>>;

public sealed class CreateOrderHandler(IIdentityService identityService, DaprWorkflowClient daprWorkflowClient)
    : ICommandHandler<CreateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var buyerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(buyerId);

        var instanceId = Guid.NewGuid();

        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(CreateOrderWorkflow),
            input: new CreateOrderWorkflowRequest(Guid.Parse(buyerId), request.Note, request.PaymentMethod),
            instanceId: instanceId.ToString());

        return instanceId;
    }
}
