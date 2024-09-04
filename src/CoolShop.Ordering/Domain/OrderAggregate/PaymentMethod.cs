using System.Text.Json.Serialization;

namespace CoolShop.Ordering.Domain.OrderAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMethod : byte
{
    Cash = 0,
    CreditCard = 1,
    EWallet = 2,
    BankTransfer = 3
}
