using CoolShop.Cart.Domain;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoolShop.Cart.Features.Delete;

public sealed class DeleteBasketEndpoint : IEndpoint<NoContent, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets", async (ISender sender) => await HandleAsync(sender))
            .Produces<NoContent>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Basket))
            .WithName("Delete Basket")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<NoContent> HandleAsync(ISender request, CancellationToken cancellationToken = default)
    {
        await request.Send(new DeleteBasketCommand(), cancellationToken);

        return TypedResults.NoContent();
    }
}
