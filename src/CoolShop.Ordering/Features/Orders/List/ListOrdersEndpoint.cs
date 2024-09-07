using CoolShop.Ordering.Domain.OrderAggregate;

namespace CoolShop.Ordering.Features.Orders.List;

public sealed record ListOrdersRequest(int PageIndex, int PageSize);

public sealed record ListOrdersResponse(PagedInfo PagedInfo, List<OrderDto> Orders);

public sealed class ListOrdersEndpoint : IEndpoint<Ok<ListOrdersResponse>, ListOrdersRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (
                ISender sender,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize), sender))
            .Produces<Ok<ListOrdersResponse>>()
            .ProducesValidationProblem()
            .WithTags(nameof(Order))
            .WithName("List Order")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok<ListOrdersResponse>> HandleAsync(ListOrdersRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new ListOrdersQuery(request.PageIndex, request.PageSize);

        var orders = await sender.Send(query, cancellationToken);

        var response = new ListOrdersResponse(orders.PagedInfo, orders.Value.ToList());

        return TypedResults.Ok(response);
    }
}
