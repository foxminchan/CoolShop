using CoolShop.Catalog.Application.Products.Create;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Filters;
using CoolShop.Shared.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed class Create : IEndpoint<Created<Guid>, CreateProductRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (ISender sender,
                        [FromForm] string name,
                        [FromForm] string? description,
                        [FromForm] decimal price,
                        [FromForm] decimal priceSale,
                        [FromForm] Status status,
                        [FromForm] Guid categoryId,
                        [FromForm] Guid brandId,
                        [FromForm] Guid inventoryId,
                        IFormFile? image)
                    => await HandleAsync(
                        new(name, description, image, price, priceSale, status, categoryId, brandId, inventoryId),
                        sender))
            .AddEndpointFilter<FileValidationFilter>()
            .Produces<Created<Guid>>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .DisableAntiforgery()
            .WithTags(nameof(Product))
            .WithName("Create Product")
            .MapToApiVersion(new(1, 0));
    }

    public async Task<Created<Guid>> HandleAsync(CreateProductRequest request, ISender sender,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Image,
            request.Price,
            request.PriceSale,
            request.Status,
            request.CategoryId,
            request.BrandId,
            request.InventoryId);

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/api/v1/products/{result.Value}", result.Value);
    }
}
