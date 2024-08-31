using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Catalog.Infrastructure.Storage;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.RemoveImage;

public sealed class RemoveImageHandler(IRepository<Product> repository, ILocalStorage storage)
    : ICommandHandler<RemoveImageCommand, Result>
{
    public async Task<Result> Handle(RemoveImageCommand request, CancellationToken cancellationToken = default)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        storage.RemoveFile(product.Image);
        product.RemoveImage();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
