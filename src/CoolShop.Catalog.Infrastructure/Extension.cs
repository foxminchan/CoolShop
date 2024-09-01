using CoolShop.Catalog.Infrastructure.Data;
using CoolShop.Catalog.Infrastructure.Storage;

namespace CoolShop.Catalog.Infrastructure;

public static class Extension
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddPersistence();

        builder.AddLocalStorage();

        return builder;
    }
}
