﻿using CoolShop.Catalog.Application.Categories.Update;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed class Update : IEndpoint<Ok, UpdateCategoryRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/categories",
                async (UpdateCategoryRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Category))
            .WithName("Update Category")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(UpdateCategoryRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new UpdateCategoryCommand(request.Id, request.Name), cancellationToken);

        return TypedResults.Ok();
    }
}
