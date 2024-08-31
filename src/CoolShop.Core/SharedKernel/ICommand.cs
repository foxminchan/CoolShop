using MediatR;

namespace CoolShop.Core.SharedKernel;

public interface ICommand<out TResponse> : IRequest<TResponse>;
