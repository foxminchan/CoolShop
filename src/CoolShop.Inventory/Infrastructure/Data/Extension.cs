using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace CoolShop.Inventory.Infrastructure.Data;

internal static class Extension
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<InventoryContext>(ServiceName.Database.Inventory, configureDbContextOptions:
            dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder
                    .UseNpgsql(optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(typeof(InventoryContext).Assembly.FullName);
                        optionsBuilder.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    })
                    .UseExceptionProcessor()
                    .UseSnakeCaseNamingConvention();
            });

        builder.Services.AddMigration<InventoryContext, InventoryContextSeed>();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(InventoryRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(InventoryRepository<>));

        return builder;
    }
}
