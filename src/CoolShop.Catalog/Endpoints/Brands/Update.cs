using CoolShop.Catalog.Application.Brands.Update;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed class Update : IEndpoint<Ok, UpdateBrandRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/brands",
                async (UpdateBrandRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Brand))
            .WithName("Update Brand")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Ok> HandleAsync(UpdateBrandRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new UpdateBrandCommand(request.Id, request.Name), cancellationToken);

        return TypedResults.Ok();
    }
}
