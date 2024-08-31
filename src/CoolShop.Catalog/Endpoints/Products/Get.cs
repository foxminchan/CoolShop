using CoolShop.Catalog.Application.Products;
using CoolShop.Catalog.Application.Products.Get;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class Get : IEndpoint<Ok<ProductDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<ProductDto>>()
            .WithTags(nameof(Product))
            .WithName("Get Product")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ProductDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var product = await sender.Send(new GetProductQuery(id), cancellationToken);

        return TypedResults.Ok(product.Value);
    }
}
