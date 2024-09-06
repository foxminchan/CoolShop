namespace CoolShop.Ordering.Extensions;

internal static partial class OrderingTraceExtension
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "[[{Event}] - Order {OrderId} created")]
    public static partial void LogOrderCreated(ILogger logger, string? @event, Guid orderId);

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "[[{Event}] - Order {OrderId} shipped")]
    public static partial void LogOrderShipped(ILogger logger, string? @event, Guid orderId);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "[[{Event}] - Order {OrderId} cancelled")]
    public static partial void LogOrderCancelled(ILogger logger, string? @event, Guid orderId);

    [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "[[{Event}] - Order {OrderId} held")]
    public static partial void LogOrderHeld(ILogger logger, string? @event, Guid orderId);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "[[{Event}] - Order {OrderId} refunded")]
    public static partial void LogOrderRefunded(ILogger logger, string? @event, Guid orderId);
}
