namespace CoolShop.Promotion.Repositories;

public interface IPromotionRepository
{
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task UpdateAsync(FilterDefinition<Coupon> filter, Coupon coupon, CancellationToken cancellationToken = default);

    Task<IEnumerable<Coupon>> ListAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<Coupon?> GetAsync(FilterDefinition<Coupon> filter, CancellationToken cancellationToken = default);
}
