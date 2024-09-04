using Ardalis.Specification.EntityFrameworkCore;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Ordering.Infrastructure.Data;

public sealed class OrderingRepository<T>(OrderingContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;
