using CoolShop.Catalog.Application.Products.RemoveImage;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class RemoveImage : IEndpoint<Ok, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/products/{id:guid}/remove-image",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Product))
            .WithName("Remove Product Image")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new RemoveImageCommand(id), cancellationToken);

        return TypedResults.Ok();
    }
}
