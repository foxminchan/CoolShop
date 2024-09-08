namespace CoolShop.Promotion.Repositories;

public sealed class PromotionRepository(IMongoCollection<Coupon> collection) : IPromotionRepository
{
    public async Task AddAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        await collection.InsertOneAsync(coupon, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(FilterDefinition<Coupon> filter, Coupon coupon,
        CancellationToken cancellationToken = default)
    {
        await collection.ReplaceOneAsync(filter, coupon, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Coupon>> ListAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await collection.Find(x => x.ProductIds!.Contains(productId)).ToListAsync(cancellationToken);
    }

    public async Task<Coupon?> GetAsync(FilterDefinition<Coupon> filter, CancellationToken cancellationToken = default)
    {
        return await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
}
