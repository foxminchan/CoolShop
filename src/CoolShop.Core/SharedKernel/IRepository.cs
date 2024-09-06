using CoolShop.Core.SeedWork;

namespace CoolShop.Core.SharedKernel;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot;
