using Ardalis.GuardClauses;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Infrastructure.Storage;

namespace CoolShop.Catalog.Application.Products.RemoveImage;

public sealed class RemoveImageHandler(IRepository<Product> repository, IAzuriteService storage)
    : ICommandHandler<RemoveImageCommand, Result>
{
    public async Task<Result> Handle(RemoveImageCommand request, CancellationToken cancellationToken = default)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        await storage.DeleteFileAsync(product.Image, cancellationToken);
        product.RemoveImage();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
