using Ardalis.Result;
using CoolShop.Core.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace CoolShop.Catalog.Application.Products.Update;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    IFormFile? Image,
    decimal Price,
    decimal PriceSale,
    Guid CategoryId,
    Guid BrandId,
    Guid InventoryId) : ICommand<Result>;
