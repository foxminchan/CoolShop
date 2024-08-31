using Ardalis.Result;
using CoolShop.Catalog.Application.Products;

namespace CoolShop.Catalog.Endpoints.Products;

public sealed record ListProductsResponse(PagedInfo PagedInfo, List<ProductDto> Products);
