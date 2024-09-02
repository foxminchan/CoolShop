using CoolShop.Inventory.Domain.SupplierAggregator;

namespace CoolShop.Inventory.Features.Suppliers.Update;

public sealed record UpdateSupplierRequest(Guid Id, string? Name, string? PhoneNumber);

public sealed class UpdateSupplierEndpoint : IEndpoint<Ok, UpdateSupplierRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/suppliers",
                async (UpdateSupplierRequest request, ISender sender) => await HandleAsync(request, sender))
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags(nameof(Supplier))
            .WithName("Update Supplier")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(UpdateSupplierRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateSupplierCommand(request.Id, request.Name, request.PhoneNumber);

        await sender.Send(command, cancellationToken);

        return TypedResults.Ok();
    }
}
