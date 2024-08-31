using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Categories.Update;

public sealed class UpdateCategoryHandler(IRepository<Category> repository)
    : ICommandHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, category);

        category.Update(request.Name);

        await repository.UpdateAsync(category, cancellationToken);

        return Result.Success();
    }
}
