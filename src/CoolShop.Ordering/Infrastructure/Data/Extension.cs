﻿using CoolShop.Core.SharedKernel;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace CoolShop.Ordering.Infrastructure.Data;

internal static class Extension
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMigration<OrderingContext>();

        builder.AddNpgsqlDbContext<OrderingContext>(ServiceName.Database.Ordering, configureDbContextOptions:
            dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder
                    .UseNpgsql(optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(typeof(OrderingContext).Assembly.FullName);
                        optionsBuilder.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    })
                    .UseExceptionProcessor()
                    .UseSnakeCaseNamingConvention();
            });

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(OrderingRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(OrderingRepository<>));

        return builder;
    }
}