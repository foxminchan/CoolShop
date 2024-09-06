namespace CoolShop.Catalog.Application.Products.IncreaseQuantity;

public sealed record IncreaseQuantityCommand(Dictionary<Guid, int> ProductQuantities) : ICommand<Result>;
