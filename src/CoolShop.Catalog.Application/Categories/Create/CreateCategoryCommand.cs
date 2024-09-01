namespace CoolShop.Catalog.Application.Categories.Create;

public sealed record CreateCategoryCommand(string Name) : ICommand<Result<Guid>>;
