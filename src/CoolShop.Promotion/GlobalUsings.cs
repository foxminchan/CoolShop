global using Ardalis.GuardClauses;
global using Ardalis.Result;
global using CoolShop.Constants;
global using CoolShop.Core.SeedWork;
global using CoolShop.Core.SharedKernel;
global using CoolShop.Promotion.Domain;
global using CoolShop.Promotion.Extensions;
global using CoolShop.Promotion.Repositories;
global using CoolShop.ServiceDefaults;
global using CoolShop.Shared.ActivityScope;
global using CoolShop.Shared.Converters;
global using CoolShop.Shared.Endpoints;
global using CoolShop.Shared.Exceptions;
global using CoolShop.Shared.Metrics;
global using CoolShop.Shared.Pipelines;
global using CoolShop.Shared.Versioning;
global using Dapr.Client;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.Http.Json;
global using MongoDB.Bson;
global using MongoDB.Driver;