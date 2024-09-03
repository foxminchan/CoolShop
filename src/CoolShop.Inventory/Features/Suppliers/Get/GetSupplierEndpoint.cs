using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Get;

public sealed class GetSupplierEndpoint : IEndpoint<Ok<SupplierDto>, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/suppliers/{id:guid}", async (Guid id, ISender sender) => await HandleAsync(id, sender))
            .Produces<Ok<SupplierDto>>()
            .WithTags(nameof(Supplier))
            .WithName("Get Supplier")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok<SupplierDto>> HandleAsync(Guid id, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetSupplierQuery(id), cancellationToken);

        return TypedResults.Ok(result.Value?.ToDto());
    }
}
