using CoolShop.Catalog.Application.Categories.Create;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed class Create : IEndpoint<Created<Guid>, CreateCategoryRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/categories",
                async (CreateCategoryRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Category))
            .WithName("Create Category")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateCategoryRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CreateCategoryCommand(request.Name), cancellationToken);

        return TypedResults.Created($"/api/v1/categories/{result.Value}", result.Value);
    }
}
