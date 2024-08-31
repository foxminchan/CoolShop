using System.Text.Json.Serialization;

namespace CoolShop.Catalog.Domain.ProductAggregator;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status : byte
{
    ComingSoon = 0,
    InStock = 1,
    OutOfStock = 2,
    Discontinued = 3
}
