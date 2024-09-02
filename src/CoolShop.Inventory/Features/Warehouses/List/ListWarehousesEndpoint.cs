using CoolShop.Inventory.Domain.WarehouseAggregator;

namespace CoolShop.Inventory.Features.Warehouses.List;

public sealed record ListWarehousesRequest(
    int PageIndex,
    int PageSize,
    string? Name,
    string? OrderBy,
    bool IsDescending);

public sealed record ListWarehousesResponse(PagedInfo PagedInfo, List<WarehouseDto> Warehouses);

public sealed class ListWarehousesEndpoint : IEndpoint<Ok<ListWarehousesResponse>, ListWarehousesRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses",
                async (ISender sender,
                        string? name,
                        string? orderBy,
                        bool isDescending = true,
                        int pageIndex = 1,
                        int pageSize = 20) =>
                    await HandleAsync(new(pageIndex, pageSize, name, orderBy, isDescending), sender))
            .Produces<Ok<ListWarehousesResponse>>()
            .ProducesValidationProblem()
            .WithTags(nameof(Warehouse))
            .WithName("List Warehouses")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListWarehousesResponse>> HandleAsync(ListWarehousesRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new ListWarehousesQuery(request.PageIndex, request.PageSize, request.Name, request.OrderBy,
            request.IsDescending);

        var result = await sender.Send(query, cancellationToken);

        var warehouses = result.Value.ToDtos();

        var response = new ListWarehousesResponse(result.PagedInfo, warehouses.ToList());

        return TypedResults.Ok(response);
    }
}
