namespace CoolShop.Catalog.Application.Products.GetStatusRange;

public sealed record GetStatusRangeQuery(List<Guid> Ids) : IQuery<Result<Dictionary<Guid, bool>>>;
