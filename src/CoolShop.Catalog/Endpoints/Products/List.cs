using CoolShop.Catalog.Application.Products.List;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class List : IEndpoint<Ok<ListProductsResponse>, ListProductsRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
                async (ISender sender,
                        Status[]? statuses,
                        Guid?[]? categoryId,
                        Guid?[]? brandId,
                        string? orderBy,
                        int pageIndex = 1,
                        int pageSize = 20,
                        bool isDescending = false) =>
                    await HandleAsync(new(pageIndex, pageSize, orderBy, isDescending, statuses, categoryId, brandId),
                        sender))
            .Produces<Ok<ListProductsResponse>>()
            .ProducesValidationProblem()
            .WithTags(nameof(Product))
            .WithName("List Products")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListProductsResponse>> HandleAsync(ListProductsRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var query = new ListProductsQuery(
            request.PageIndex,
            request.PageSize,
            request.OrderBy,
            request.IsDescending,
            request.Statuses,
            request.CategoryId,
            request.BrandId);

        var result = await sender.Send(query, cancellationToken);

        var response = new ListProductsResponse(result.PagedInfo, result.Value.ToList());

        return TypedResults.Ok(response);
    }
}
