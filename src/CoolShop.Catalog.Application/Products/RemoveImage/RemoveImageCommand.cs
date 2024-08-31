using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.RemoveImage;

public sealed record RemoveImageCommand(Guid Id) : ICommand<Result>;
