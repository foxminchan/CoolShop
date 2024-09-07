using CoolShop.Ordering.Domain.BuyerAggregate;

namespace CoolShop.Ordering.Features.Buyers.Create;

public sealed record CreateBuyerRequest(
    string? PhoneNumber,
    string? Email,
    string? Street,
    string? City,
    string? Province);

public sealed class CreateBuyerEndpoint : IEndpoint<Created<Guid>, CreateBuyerRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/buyers",
                async (CreateBuyerRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Buyer))
            .WithName("Create Buyer")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Created<Guid>> HandleAsync(CreateBuyerRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateBuyerCommand(
            request.PhoneNumber,
            request.Email,
            request.Street,
            request.City,
            request.Province);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/buyer/{result.Value}", result.Value);
    }
}
