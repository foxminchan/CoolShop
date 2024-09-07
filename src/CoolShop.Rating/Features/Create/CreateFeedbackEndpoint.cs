namespace CoolShop.Rating.Features.Create;

public sealed record CreateFeedbackRequest(Guid ProductId, int Rating, string? Comment);

public sealed class CreateFeedbackEndpoint : IEndpoint<Created<ObjectId>, CreateFeedbackRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/feedbacks",
                async (ISender sender, CreateFeedbackRequest request) => await HandleAsync(request, sender))
            .Produces<Created<ObjectId>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Feedback))
            .WithName("Create Feedback")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();
    }

    public async Task<Created<ObjectId>> HandleAsync(CreateFeedbackRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateFeedbackCommand(request.ProductId, request.Rating, request.Comment);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/{result.Value}", result.Value);
    }
}
