﻿using CoolShop.Catalog.Application;
using CoolShop.Catalog.Infrastructure;
using CoolShop.ServiceDefaults;
using CoolShop.Shared.Converters;
using CoolShop.Shared.Endpoints;
using CoolShop.Shared.Exceptions;
using CoolShop.Shared.Versioning;
using Microsoft.AspNetCore.Http.Json;

namespace CoolShop.Catalog.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults()
            .AddInfrastructure()
            .AddApplication();

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.Converters.Add(new StringTrimmerJsonConverter());
        });

        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<UniqueConstraintExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.AddOpenApi();
        builder.AddVersioning();
        builder.AddEndpoints(typeof(global::Program));

        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
        builder.Services.AddDaprClient();
    }
}