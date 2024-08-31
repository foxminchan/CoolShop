using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Brands.Update;

public sealed class UpdateBrandHandler(IRepository<Brand> repository)
    : ICommandHandler<UpdateBrandCommand, Result>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        product.Update(request.Name);

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
