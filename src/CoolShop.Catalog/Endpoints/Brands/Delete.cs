using CoolShop.Catalog.Application.Brands.Delete;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed class Delete : IEndpoint<NoContent, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/brands/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<NoContent>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Brand))
            .WithName("Delete Brand")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<NoContent> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteBrandCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}
