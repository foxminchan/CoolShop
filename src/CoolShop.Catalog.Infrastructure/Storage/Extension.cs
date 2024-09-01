using Microsoft.Extensions.DependencyInjection;

namespace CoolShop.Catalog.Infrastructure.Storage;

public static class Extension
{
    public static void AddLocalStorage(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILocalStorage, LocalStorage>();
    }
}
