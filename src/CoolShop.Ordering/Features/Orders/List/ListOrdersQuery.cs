using CoolShop.Ordering.Domain.OrderAggregate;
using CoolShop.Ordering.Domain.OrderAggregate.Specifications;

namespace CoolShop.Ordering.Features.Orders.List;

public sealed record ListOrdersQuery(int PageIndex, int PageSize) : IQuery<PagedResult<IEnumerable<OrderDto>>>;

public sealed class ListOrdersHandler(
    IReadRepository<Order> repository,
    IIdentityService identityService) : IQueryHandler<ListOrdersQuery, PagedResult<IEnumerable<OrderDto>>>
{
    public async Task<PagedResult<IEnumerable<OrderDto>>> Handle(ListOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var customerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(customerId);

        List<Order> orders;
        int totalRecords;

        if (identityService.IsAdminRole())
        {
            OrderFilterSpec spec = new(request.PageIndex, request.PageSize);
            orders = await repository.ListAsync(spec, cancellationToken);
            totalRecords = await repository.CountAsync(spec, cancellationToken);
        }
        else
        {
            OrderFilterSpec spec = new(Guid.Parse(customerId), request.PageIndex, request.PageSize);
            orders = await repository.ListAsync(spec, cancellationToken);
            totalRecords = await repository.CountAsync(spec, cancellationToken);
        }

        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);
        PagedInfo pagedInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, orders.ToOrderDtos());
    }
}
