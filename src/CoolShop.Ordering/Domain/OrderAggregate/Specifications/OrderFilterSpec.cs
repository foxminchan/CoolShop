using Ardalis.Specification;

namespace CoolShop.Ordering.Domain.OrderAggregate.Specifications;

public sealed class OrderFilterSpec : Specification<Order>
{
    public OrderFilterSpec(Guid? id)
    {
        Query.Where(x => x.Id == id && x.Status == Status.Pending);
    }

    public OrderFilterSpec(Guid? id, Guid? buyerId)
    {
        Query.Where(x => x.Id == id && x.BuyerId == buyerId);
    }

    public OrderFilterSpec(int pageIndex, int pageSize)
    {
        Query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }

    public OrderFilterSpec(Guid? buyerId, int pageIndex, int pageSize)
    {
        Query
            .Where(x => x.BuyerId == buyerId)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
