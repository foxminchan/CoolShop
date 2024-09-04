using CoolShop.Catalog.Application.Brands;
using CoolShop.Catalog.Application.Brands.Get;

namespace CoolShop.Catalog.Endpoints.Brands;

public sealed class Get : IEndpoint<Ok<BrandDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/brands/{id:guid}",
                async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<BrandDto>>()
            .WithTags(nameof(Brand))
            .WithName("Get Brand")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<BrandDto>> HandleAsync(Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        var brand = await sender.Send(new GetBrandQuery(id), cancellationToken);

        return TypedResults.Ok(brand.Value);
    }
}
