namespace CoolShop.Inventory.Features.Inventories.List;

public sealed record ListInventoriesRequest(int PageIndex, int PageSize, string? SortBy, bool IsDescending);

public sealed record ListInventoriesResponse(PagedInfo PagedInfo, List<InventoryDto> Inventories);

public sealed class ListInventoriesEndpoint : IEndpoint<Ok<ListInventoriesResponse>, ListInventoriesRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/inventories",
                async (ISender sender,
                        string? orderBy,
                        int pageIndex = 1,
                        int pageSize = 20,
                        bool isDescending = false) =>
                    await HandleAsync(new(pageIndex, pageSize, orderBy, isDescending), sender))
            .Produces<Ok<ListInventoriesResponse>>()
            .ProducesValidationProblem()
            .WithTags(nameof(Inventory))
            .WithName("List Inventory")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListInventoriesResponse>> HandleAsync(ListInventoriesRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new ListInventoriesQuery(request.PageIndex, request.PageSize, request.SortBy, request.IsDescending);

        var result = await sender.Send(query, cancellationToken);

        var inventories = result.Value.ToDtos();

        var response = new ListInventoriesResponse(result.PagedInfo, inventories.ToList());

        return TypedResults.Ok(response);
    }
}
