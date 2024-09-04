using CoolShop.Ordering.Domain.BuyerAggregate;

namespace CoolShop.Ordering.Features.Buyers;

public static class EntityToDto
{
    public static BuyerDto ToDto(this Buyer buyer)
    {
        return new(buyer.Id, buyer.Name, $"{buyer.Address?.Street}, {buyer.Address?.City}, {buyer.Address?.Province}");
    }
}
