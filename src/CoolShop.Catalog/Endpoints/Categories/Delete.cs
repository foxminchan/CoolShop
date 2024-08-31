﻿using CoolShop.Catalog.Application.Categories.Delete;
using CoolShop.Catalog.Domain;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoolShop.Catalog.Endpoints.Categories;

public sealed class Delete : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Category))
            .WithName("Delete Category")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteCategoryCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}