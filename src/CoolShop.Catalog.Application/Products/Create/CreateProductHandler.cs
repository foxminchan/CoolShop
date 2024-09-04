using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Infrastructure.Storage;

namespace CoolShop.Catalog.Application.Products.Create;

public sealed class CreateProductHandler(IRepository<Product> repository, IAzuriteService storage)
    : ICommandHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await UploadProductImagesAsync(request.Image, cancellationToken);

        var product = new Product(
            request.Name,
            request.Description,
            imageUrl,
            request.Price,
            request.PriceSale,
            request.Status,
            request.CategoryId,
            request.BrandId,
            request.InventoryId);

        var result = await repository.AddAsync(product, cancellationToken);

        return result.Id;
    }

    private async Task<string?> UploadProductImagesAsync(IFormFile? imageFile, CancellationToken cancellationToken)
    {
        if (imageFile is null)
        {
            return null;
        }

        var imageUrl = await storage.UploadFileAsync(imageFile, cancellationToken);

        return imageUrl;
    }
}
