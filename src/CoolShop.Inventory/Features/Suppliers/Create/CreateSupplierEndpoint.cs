using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Create;

public sealed record CreateSupplierRequest(string? Name, string? PhoneNumber);

public sealed class CreateSupplierEndpoint : IEndpoint<Created<Guid>, CreateSupplierRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/suppliers",
                async (CreateSupplierRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(nameof(Supplier))
            .WithName("Create Suppliers")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateSupplierRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateSupplierCommand(request.Name, request.PhoneNumber);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/suppliers/{result.Value}", result.Value);
    }
}
