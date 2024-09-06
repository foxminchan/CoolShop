namespace CoolShop.Catalog.Application.Products.AddRating;

public sealed record AddRatingCommand(Guid ProductId, int Rating) : ICommand<Result>;
