using CoolShop.Catalog.Domain.IntegrationEvents;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.AddRating;

public sealed class AddRatingHandler(IRepository<Product> repository, DaprClient daprClient)
    : ICommandHandler<AddRatingCommand, Result>
{
    public async Task<Result> Handle(AddRatingCommand command, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.NotFound();
        }

        product.AddRating(command.Rating);

        await repository.SaveChangesAsync(cancellationToken);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogUpdatedIntegrationEvent),
            new CatalogUpdatedIntegrationEvent(),
            cancellationToken);

        return Result.Success();
    }
}
