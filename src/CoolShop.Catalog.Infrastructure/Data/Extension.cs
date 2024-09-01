using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoolShop.Catalog.Infrastructure.Data;

public static class Extension
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>(ServiceName.Database.Catalog, configureDbContextOptions:
            dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder
                    .UseNpgsql(optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);
                        optionsBuilder.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    })
                    .UseExceptionProcessor()
                    .UseSnakeCaseNamingConvention();
            });

        builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(CatalogRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(CatalogRepository<>));

        return builder;
    }
}
