using Ardalis.GuardClauses;
using CoolShop.Catalog.Domain;

namespace CoolShop.Catalog.Application.Categories.Delete;

public sealed class DeleteCategoryHandler(IRepository<Category> repository)
    : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, category);

        category.Delete();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
