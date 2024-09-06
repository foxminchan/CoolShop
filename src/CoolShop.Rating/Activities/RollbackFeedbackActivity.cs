namespace CoolShop.Rating.Activities;

public sealed class RollbackFeedbackActivity(IRatingRepository repository, ILoggerFactory loggerFactory)
    : WorkflowActivity<RollbackFeedbackActivityData, object?>
{
    private readonly ILogger<RollbackFeedbackActivity> _logger = loggerFactory.CreateLogger<RollbackFeedbackActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, RollbackFeedbackActivityData input)
    {
        _logger.LogInformation("[{Activity}] - Rolling back feedback for product {ProductId} with rating {Rating}",
            nameof(RollbackFeedbackActivity), input.Feedback.ProductId, input.Feedback.Rating);

        switch (input.Event)
        {
            case nameof(CatalogAddedRatingIntegrationEvent):
                await repository.DeleteAsync(Builders<Feedback>.Filter.Eq(x => x.ProductId, input.Feedback.ProductId));
                break;
            case nameof(CatalogRemovedRatingIntegrationEvent):
                await repository.AddAsync(input.Feedback);
                break;
            default:
                throw new ArgumentException("Unknown event type", nameof(input.Event));
        }

        _logger.LogInformation(
            "[{Activity}] - Feedback for product {ProductId} with rating {Rating} has been rolled back",
            nameof(RollbackFeedbackActivity), input.Feedback.ProductId, input.Feedback.Rating);

        return default;
    }
}
