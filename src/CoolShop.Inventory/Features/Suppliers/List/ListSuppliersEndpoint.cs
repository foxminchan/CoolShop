using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.List;

public sealed record ListSuppliersRequest(int PageIndex, int PageSize, string? SearchQuery);

public sealed record ListSuppliersResponse(PagedInfo PagedInfo, List<SupplierDto> Suppliers);

public sealed class ListSuppliersEndpoint : IEndpoint<Ok<ListSuppliersResponse>, ListSuppliersRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/suppliers",
                async (ISender sender,
                    string? searchQuery,
                    int pageIndex = 1,
                    int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, searchQuery), sender))
            .Produces<Ok<ListSuppliersResponse>>()
            .ProducesValidationProblem()
            .WithTags(nameof(Supplier))
            .WithName("List Suppliers")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListSuppliersResponse>> HandleAsync(ListSuppliersRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new ListSuppliersQuery(request.PageIndex, request.PageSize, request.SearchQuery);

        var result = await sender.Send(query, cancellationToken);

        var suppliers = result.Value.ToDtos();

        var response = new ListSuppliersResponse(result.PagedInfo, suppliers.ToList());

        return TypedResults.Ok(response);
    }
}
