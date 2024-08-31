﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using CoolShop.Catalog.Domain.ProductAggregator;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Products.Discontinued;

public sealed class DiscontinuedProductHandler(IRepository<Product> repository)
    : ICommandHandler<DiscontinuedProductCommand, Result>
{
    public async Task<Result> Handle(DiscontinuedProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, product);

        product.MarkDiscontinued();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}