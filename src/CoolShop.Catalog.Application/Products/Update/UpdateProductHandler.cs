using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Infrastructure.Storage;
using CoolShop.Core.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace CoolShop.Catalog.Application.Products.Update;

public sealed class UpdateProductHandler(IRepository<Product> repository, ILocalStorage storage)
    : ICommandHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        string? newImage = null;
        if (request.Image is not null)
        {
            newImage = await ProcessUpdateImage(product, request.Image);
        }

        product.Update(
            request.Name,
            request.Description,
            newImage,
            request.Price,
            request.PriceSale,
            request.CategoryId,
            request.BrandId,
            request.InventoryId
        );

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


    private async Task<string> ProcessUpdateImage(Product product, IFormFile image)
    {
        if (product.Image is not null)
        {
            storage.RemoveFile(product.Image);
        }

        var newImage = await storage.UploadFileAsync(image);

        return newImage;
    }
}
