namespace CoolShop.Rating.Activities;

public sealed class CreateFeedbackActivity(
    DaprClient daprClient,
    IRatingRepository repository,
    ILoggerFactory loggerFactory) : WorkflowActivity<Feedback, Feedback>
{
    private readonly ILogger<CreateFeedbackActivity> _logger = loggerFactory.CreateLogger<CreateFeedbackActivity>();

    public override async Task<Feedback> RunAsync(WorkflowActivityContext context, Feedback input)
    {
        _logger.LogInformation("[{Activity}] - Creating feedback for product {ProductId} with rating {Rating}",
            nameof(CreateFeedbackActivity), input.ProductId, input.Rating);

        await repository.AddAsync(input);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogAddedRatingIntegrationEvent).ToLowerInvariant(),
            new CatalogAddedRatingIntegrationEvent(input.ProductId, input.Rating));

        _logger.LogInformation("[{Activity}] - Published {Event} event", nameof(CreateFeedbackActivity),
            nameof(CatalogAddedRatingIntegrationEvent));

        return input;
    }
}
