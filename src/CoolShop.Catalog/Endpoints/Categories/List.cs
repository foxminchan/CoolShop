using CoolShop.Catalog.Application.Categories.List;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed class List : IEndpoint<Ok<ListCategoriesResponse>, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async (ISender sender) => await HandleAsync(sender))
            .Produces<Ok<ListCategoriesResponse>>()
            .WithTags(nameof(Category))
            .WithName("List Categories")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListCategoriesResponse>> HandleAsync(ISender request,
        CancellationToken cancellationToken = default)
    {
        var categories = await request.Send(new ListCategoriesQuery(), cancellationToken);

        return TypedResults.Ok(new ListCategoriesResponse(categories.Value.ToList()));
    }
}
