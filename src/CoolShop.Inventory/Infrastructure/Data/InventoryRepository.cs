using Ardalis.Specification.EntityFrameworkCore;

namespace CoolShop.Inventory.Infrastructure.Data;

public sealed class InventoryRepository<T>(InventoryContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot;
