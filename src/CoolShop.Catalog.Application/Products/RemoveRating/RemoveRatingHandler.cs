using CoolShop.Catalog.Domain.IntegrationEvents;
using CoolShop.Catalog.Domain.ProductAggregator;

namespace CoolShop.Catalog.Application.Products.RemoveRating;

public sealed class RemoveRatingHandler(IRepository<Product> repository, DaprClient daprClient)
    : ICommandHandler<RemoveRatingCommand, Result>
{
    public async Task<Result> Handle(RemoveRatingCommand command, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.NotFound();
        }

        product.RemoveRating(command.Rating);

        await repository.SaveChangesAsync(cancellationToken);

        await daprClient.PublishEventAsync(
            ServiceName.Dapr.PubSub,
            nameof(CatalogUpdatedIntegrationEvent),
            new CatalogUpdatedIntegrationEvent(),
            cancellationToken);

        return Result.Success();
    }
}
