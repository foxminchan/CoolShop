namespace CoolShop.Rating.Features.List;

public sealed record ListFeedbackQuery(Guid ProductId, int PageIndex, int PageSize)
    : IQuery<PagedResult<IEnumerable<Feedback>>>;

public sealed class ListFeedbackHandler(IRatingRepository repository)
    : IQueryHandler<ListFeedbackQuery, PagedResult<IEnumerable<Feedback>>>
{
    public async Task<PagedResult<IEnumerable<Feedback>>> Handle(ListFeedbackQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Feedback>.Filter.Eq(x => x.ProductId, request.ProductId);
        var feedbacks = await repository.ListAsync(filter, request.PageIndex, request.PageSize, cancellationToken);

        var totalRecords = await repository.CountAsync(filter, cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);
        var pagedInfo = new PagedInfo(request.PageIndex, request.PageSize, totalPages, totalRecords);

        return new(pagedInfo, feedbacks);
    }
}
