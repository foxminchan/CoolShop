using CoolShop.Core.SeedWork;

namespace CoolShop.Core.SharedKernel;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot;
