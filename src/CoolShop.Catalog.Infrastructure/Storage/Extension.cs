using Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CoolShop.Catalog.Infrastructure.Storage;

public static class Extension
{
    public static void AddLocalStorage(this IHostApplicationBuilder builder)
    {
        builder.Services.AddResiliencePipeline(nameof(Storage), resiliencePipelineBuilder => resiliencePipelineBuilder
            .AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder().Handle<RequestFailedException>(),
                Delay = TimeSpan.FromSeconds(2),
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Constant
            })
            .AddTimeout(TimeSpan.FromSeconds(10)));

        builder.Services.AddSingleton<IAzuriteService, AzuriteService>();
    }
}
