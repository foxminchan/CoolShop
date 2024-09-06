namespace CoolShop.Catalog.Application.Products.RemoveRating;

public sealed record RemoveRatingCommand(Guid ProductId, int Rating) : ICommand<Result>;
