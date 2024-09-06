using CoolShop.Catalog.Grpc;
using CoolShop.Ordering.Domain.OrderAggregate;
using CoolShop.Ordering.Domain.OrderAggregate.Specifications;

namespace CoolShop.Ordering.Features.Orders.Get;

public sealed record GetOrderQuery(Guid Id) : IQuery<Result<OrderDetailDto>>;

public sealed class GetOrderQueryHandler(
    IReadRepository<Order> repository,
    DaprClient daprClient,
    IIdentityService identityService) : IQueryHandler<GetOrderQuery, Result<OrderDetailDto>>
{
    public async Task<Result<OrderDetailDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var buyerId = identityService.GetUserIdentity();

        Guard.Against.NullOrEmpty(buyerId);

        Order? order;
        if (identityService.IsAdminRole())
        {
            order = await repository.GetByIdAsync(request.Id, cancellationToken);
        }
        else
        {
            var spec = new OrderFilterSpec(request.Id, Guid.Parse(buyerId));
            order = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        }

        if (order is null)
        {
            return Result.NotFound();
        }

        return await MapToOrderDetailDto(order, cancellationToken);
    }

    private async Task<OrderDetailDto> MapToOrderDetailDto(Order order, CancellationToken cancellationToken)
    {
        List<OrderItemDto> orderItems = [];

        foreach (var item in order.OrderItems)
        {
            var product = await daprClient.InvokeMethodAsync<ProductRequest, ProductResponse>(
                ServiceName.AppId.Catalog,
                nameof(Product.ProductClient.GetProduct),
                new() { Id = item.ProductId.ToString() },
                cancellationToken);
            orderItems.Add(new(Guid.Parse(product.Id), product.Name, item.Quantity, item.Price));
        }

        OrderDetailDto orderDetailDto = new(order.Id, order.Note, order.Status, order.TotalPrice, orderItems);

        return orderDetailDto;
    }
}
