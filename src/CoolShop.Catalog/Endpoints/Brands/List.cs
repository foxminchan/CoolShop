using CoolShop.Catalog.Application.Brands.List;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed class List : IEndpoint<Ok<ListBrandsResponse>, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/brands",
                async (ISender sender) => await HandleAsync(sender))
            .Produces<Ok<ListBrandsResponse>>()
            .WithTags(nameof(Brand))
            .WithName("List Brands")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<ListBrandsResponse>> HandleAsync(ISender sender, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new ListBrandsQuery(), cancellationToken);

        return TypedResults.Ok(new ListBrandsResponse(result.Value.ToList()));
    }
}
