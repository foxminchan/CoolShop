using System.Reflection;
using CoolShop.Shared.ActivityScope;
using CoolShop.Shared.Metrics;
using CoolShop.Shared.Pipelines;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoolShop.Catalog.Application;

public static class Extension
{
    public static void AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies([Assembly.GetExecutingAssembly()]);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(MetricsBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()], includeInternalTypes: true);

        builder.Services.AddSingleton<IActivityScope, ActivityScope>();
        builder.Services.AddSingleton<CommandHandlerMetrics>();
        builder.Services.AddSingleton<QueryHandlerMetrics>();
    }
}
