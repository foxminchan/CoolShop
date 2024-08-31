﻿using Ardalis.Result;
using CoolShop.Core.SharedKernel;

namespace CoolShop.Catalog.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand<Result>;