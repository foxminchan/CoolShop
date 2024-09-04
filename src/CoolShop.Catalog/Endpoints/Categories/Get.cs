using CoolShop.Catalog.Application.Categories;
using CoolShop.Catalog.Application.Categories.Get;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed class Get : IEndpoint<Ok<CategoryDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<CategoryDto>>()
            .WithTags(nameof(Category))
            .WithName("Get Category")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<CategoryDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetCategoryQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value);
    }
}
