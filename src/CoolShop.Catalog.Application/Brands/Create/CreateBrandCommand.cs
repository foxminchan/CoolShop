using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Brands.Create;

public sealed record CreateBrandCommand(string Name) : ICommand<Result<Guid>>;
