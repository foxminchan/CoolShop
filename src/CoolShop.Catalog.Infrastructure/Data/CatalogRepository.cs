using Ardalis.Specification.EntityFrameworkCore;
using CoolShop.Core.SeedWork;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Infrastructure.Data;

public sealed class CatalogRepository<T>(CatalogContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;
