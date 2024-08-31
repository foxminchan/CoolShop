using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Brands.Delete;

public sealed class DeleteBrandHandler(IRepository<Brand> repository)
    : ICommandHandler<DeleteBrandCommand, Result>
{
    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, brand);

        brand.Delete();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
