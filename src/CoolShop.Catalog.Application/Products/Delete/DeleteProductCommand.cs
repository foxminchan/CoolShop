namespace CoolShop.Catalog.Application.Products.Delete;

public sealed record DeleteProductCommand(Guid Id) : ICommand<Result>;
