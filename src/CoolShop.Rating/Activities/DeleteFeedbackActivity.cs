namespace CoolShop.Rating.Activities;

public sealed class DeleteFeedbackActivity(
    DaprClient daprClient,
    IRatingRepository repository,
    ILoggerFactory loggerFactory) : WorkflowActivity<Feedback, object?>
{
    private readonly ILogger<DeleteFeedbackActivity> _logger = loggerFactory.CreateLogger<DeleteFeedbackActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Feedback input)
    {
        _logger.LogInformation("[{Activity}] - Deleting feedback for product {ProductId}",
            nameof(DeleteFeedbackActivity), input.ProductId);

        await repository.DeleteAsync(Builders<Feedback>.Filter.Eq(x => x.Id, input.Id));

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogRemovedRatingIntegrationEvent).ToLowerInvariant(),
            new CatalogRemovedRatingIntegrationEvent(input.ProductId, input.Rating));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(DeleteFeedbackActivity),
            nameof(CatalogRemovedRatingIntegrationEvent));

        return default;
    }
}
