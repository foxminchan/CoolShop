using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Categories.List;

public sealed record ListCategoriesQuery : IQuery<Result<IEnumerable<CategoryDto>>>;
