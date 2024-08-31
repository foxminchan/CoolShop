using Ardalis.Result;
using CoolShop.Catalog.Domain;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Categories.Create;

public sealed class CreateCategoryHandler(IRepository<Category> repository)
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name);

        var result = await repository.AddAsync(category, cancellationToken);

        return result.Id;
    }
}
