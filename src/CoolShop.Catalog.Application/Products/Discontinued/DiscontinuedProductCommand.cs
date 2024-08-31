using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.Discontinued;

public sealed record DiscontinuedProductCommand(Guid Id) : ICommand<Result>;
