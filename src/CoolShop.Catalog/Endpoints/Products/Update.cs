using CoolShop.Catalog.Application.Products.Update;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Filters;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class Update : IEndpoint<Ok, UpdateProductRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
                async (ISender sender,
                        [FromForm] Guid id,
                        [FromForm] string name,
                        [FromForm] string? description,
                        [FromForm] decimal price,
                        [FromForm] decimal priceSale,
                        [FromForm] Guid categoryId,
                        [FromForm] Guid brandId,
                        [FromForm] Guid inventoryId,
                        IFormFile? image)
                    => await HandleAsync(
                        new(id, name, description, image, price, priceSale, categoryId, brandId, inventoryId),
                        sender))
            .AddEndpointFilter<FileValidationFilter>()
            .Produces<Ok>()
            .ProducesValidationProblem()
            .DisableAntiforgery()
            .WithTags(nameof(Product))
            .WithName("Update Product")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Ok> HandleAsync(UpdateProductRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateProductCommand(
            request.Id,
            request.Name,
            request.Description,
            request.Image,
            request.Price,
            request.PriceSale,
            request.CategoryId,
            request.BrandId,
            request.InventoryId);

        await sender.Send(command, cancellationToken);

        return TypedResults.Ok();
    }
}
