using CoolShop.Catalog.Application.Brands.Create;
using CoolShop.Catalog.Domain;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed class Create : IEndpoint<Created<Guid>, CreateBrandRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/brands",
                async (CreateBrandRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Brand))
            .WithName("Create Brand")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateBrandRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new CreateBrandCommand(request.Name), cancellationToken);

        return TypedResults.Created($"/api/v1/brands/{result.Value}", result.Value);
    }
}
