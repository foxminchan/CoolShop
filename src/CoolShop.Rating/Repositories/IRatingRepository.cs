namespace CoolShop.Rating.Repositories;

public interface IRatingRepository
{
    Task AddAsync(Feedback feedback, CancellationToken cancellationToken = default);
    Task DeleteAsync(FilterDefinition<Feedback> filter, CancellationToken cancellationToken = default);

    Task UpdateAsync(FilterDefinition<Feedback> filter, Feedback feedback,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Feedback>> ListAsync(FilterDefinition<Feedback> filter, int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);

    Task<Feedback?> GetAsync(FilterDefinition<Feedback> filter, CancellationToken cancellationToken = default);
    Task<long> CountAsync(FilterDefinition<Feedback> filter, CancellationToken cancellationToken = default);
}
