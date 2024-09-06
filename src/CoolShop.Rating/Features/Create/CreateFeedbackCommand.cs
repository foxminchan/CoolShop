namespace CoolShop.Rating.Features.Create;

public sealed record CreateFeedbackCommand(Guid ProductId, int Rating, string? Comment)
    : ICommand<Result<ObjectId>>;

public sealed class CreateFeedbackHandler(DaprWorkflowClient daprWorkflowClient, IIdentityService identityService)
    : ICommandHandler<CreateFeedbackCommand, Result<ObjectId>>
{
    public async Task<Result<ObjectId>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var buyerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(buyerId);

        var feedback = new Feedback(request.ProductId, request.Rating, request.Comment, Guid.Parse(buyerId));

        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(CreateFeedbackWorkflow),
            input: feedback,
            instanceId: feedback.Id.ToString());

        return feedback.Id;
    }
}
