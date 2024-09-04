using System.Text.Json.Serialization;

namespace CoolShop.Ordering.Domain.OrderAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status : byte
{
    Pending = 0,
    Shipped = 1,
    Cancelled = 2,
    Held = 3,
    Refund = 4
}
