﻿namespace CoolShop.Catalog.Application.Brands.Delete;

public sealed record DeleteBrandCommand(Guid Id) : ICommand<Result>;
