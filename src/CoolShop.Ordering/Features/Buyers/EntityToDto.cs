using CoolShop.Ordering.Domain.BuyerAggregate;

namespace CoolShop.Ordering.Features.Buyers;

public static class EntityToDto
{
    public static BuyerDto ToDto(this Buyer buyer)
    {
        var address = $"{buyer.Address?.Street}, {buyer.Address?.City}, {buyer.Address?.Province}";
        return new(buyer.Id, buyer.Name, buyer.Email, buyer.PhoneNumber, address);
    }
}
