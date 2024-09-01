using Ardalis.GuardClauses;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.Delete;

public sealed class DeleteProductHandler(IRepository<Product> repository)
    : ICommandHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        product.Delete();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
