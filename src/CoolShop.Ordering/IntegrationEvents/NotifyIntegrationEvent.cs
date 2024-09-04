namespace CoolShop.Ordering.IntegrationEvents;

public sealed class NotifyIntegrationEvent(string? email, string? message)
{
    public string? Email { get; init; } = email;
    public string? Message { get; init; } = message;
}
