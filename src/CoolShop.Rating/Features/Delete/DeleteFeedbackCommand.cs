namespace CoolShop.Rating.Features.Delete;

public sealed record DeleteFeedbackCommand(ObjectId Id) : ICommand<Result>;

public sealed class DeleteFeedbackHandler(IRatingRepository repository, DaprWorkflowClient daprWorkflowClient)
    : ICommandHandler<DeleteFeedbackCommand, Result>
{
    public async Task<Result> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback =
            await repository.GetAsync(Builders<Feedback>.Filter.Eq(x => x.Id, request.Id), cancellationToken);

        Guard.Against.NotFound(request.Id, feedback);

        await daprWorkflowClient.ScheduleNewWorkflowAsync(
            nameof(DeleteFeedbackWorkflow),
            input: feedback,
            instanceId: feedback.Id.ToString());

        return Result.Success();
    }
}
