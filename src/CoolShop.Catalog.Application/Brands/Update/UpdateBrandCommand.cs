namespace CoolShop.Catalog.Application.Brands.Update;

public sealed record UpdateBrandCommand(Guid Id, string Name) : ICommand<Result>;
