﻿using CoolShop.Catalog.Application.Products.Discontinued;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class Discontinued : IEndpoint<Ok, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/products/{id:guid}/discontinued",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Product))
            .WithName("Discontinue Product")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DiscontinuedProductCommand(id), cancellationToken);

        return TypedResults.Ok();
    }
}
