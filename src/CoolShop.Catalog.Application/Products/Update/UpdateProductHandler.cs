using Ardalis.GuardClauses;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Infrastructure.Storage;

namespace CoolShop.Catalog.Application.Products.Update;

public sealed class UpdateProductHandler(IRepository<Product> repository, IAzuriteService storage)
    : ICommandHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        string? newImage = null;
        if (request.Image is not null)
        {
            newImage = await ProcessUpdateImage(product, request.Image, cancellationToken);
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


    private async Task<string> ProcessUpdateImage(Product product, IFormFile image, CancellationToken cancellationToken)
    {
        if (product.Image is not null)
        {
            await storage.DeleteFileAsync(product.Image, cancellationToken);
        }

        var newImage = await storage.UploadFileAsync(image, cancellationToken);

        return newImage;
    }
}
