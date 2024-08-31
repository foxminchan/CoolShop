using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.Get;

public sealed record GetProductQuery(Guid Id) : IQuery<Result<ProductDto?>>;
