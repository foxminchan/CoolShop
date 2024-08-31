﻿using MediatR;

namespace CoolShop.Core.SharedKernel;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;
