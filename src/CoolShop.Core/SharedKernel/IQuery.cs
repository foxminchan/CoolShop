using MediatR;

namespace CoolShop.Core.SharedKernel;

public interface IQuery<out TResponse> : IRequest<TResponse>;
