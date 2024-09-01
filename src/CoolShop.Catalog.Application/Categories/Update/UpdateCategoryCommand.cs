namespace CoolShop.Catalog.Application.Categories.Update;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand<Result>;
