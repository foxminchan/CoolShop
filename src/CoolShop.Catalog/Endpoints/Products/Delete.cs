﻿using CoolShop.Catalog.Application.Products.Delete;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class Delete : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Product))
            .WithName("Delete Product")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteProductCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
