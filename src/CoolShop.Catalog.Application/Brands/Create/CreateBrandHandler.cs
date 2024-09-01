using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Brands.Create;

public sealed class CreateBrandHandler(IRepository<Brand> repository)
    : ICommandHandler<CreateBrandCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.AddAsync(new(request.Name), cancellationToken);

        return result.Id;
    }
}
