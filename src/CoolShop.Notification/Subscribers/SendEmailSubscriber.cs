using CoolShop.Notification.IntegrationEvents;

namespace CoolShop.Notification.Subscribers;

public sealed class SendEmailSubscriber : ISubscriber<NotifyIntegrationEvent, DaprClient>
{
    private const string CreateBindingOperation = "create";

    public void MapSubscriber(IEndpointRouteBuilder app)
    {
        var topic = new TopicOptions
        {
            PubsubName = ServiceName.Dapr.PubSub, Name = nameof(NotifyIntegrationEvent).ToLowerInvariant()
        };

        app.MapPost("/dapr-send-email",
                async (NotifyIntegrationEvent request, DaprClient daprClient) =>
                    await HandleAsync(request, daprClient))
            .WithTopic(topic)
            .ExcludeFromDescription();
    }

    public async Task HandleAsync(NotifyIntegrationEvent request, DaprClient daprClient,
        CancellationToken cancellationToken = default)
    {
        var email = request.Email;
        var message = request.Message;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        await daprClient.InvokeBindingAsync(
            ServiceName.Dapr.Smtp,
            CreateBindingOperation,
            message,
            new Dictionary<string, string>
            {
                ["emailFrom"] = "coolshop@example.com", ["emailTo"] = email, ["subject"] = "CoolShop Notification"
            },
            cancellationToken);
    }
}
