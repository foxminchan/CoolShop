namespace CoolShop.Ordering.Activities;

public sealed class NotifyActivity(DaprClient daprClient, ILoggerFactory loggerFactory)
    : WorkflowActivity<NotifyActivityData, object?>
{
    private readonly ILogger<NotifyActivity> _logger = loggerFactory.CreateLogger<NotifyActivity>();

    public override async Task<object?> RunAsync(WorkflowActivityContext context, NotifyActivityData input)
    {
        _logger.LogInformation("[{ActivityName}] - Sending notification to {Email}",
            nameof(NotifyActivity), input.Email);

        if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Message))
        {
            _logger.LogWarning("[{ActivityName}] - Email or message is empty", nameof(NotifyActivity));
            return null;
        }

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(NotifyIntegrationEvent).ToLowerInvariant(),
            new NotifyIntegrationEvent(input.Email, input.Message));

        return default;
    }
}
