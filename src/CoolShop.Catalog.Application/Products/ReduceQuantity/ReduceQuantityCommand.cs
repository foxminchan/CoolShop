namespace CoolShop.Catalog.Application.Products.ReduceQuantity;

public sealed record ReduceQuantityCommand(Dictionary<Guid, int> ProductQuantities) : ICommand<Result>;
