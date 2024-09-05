using CoolShop.Catalog.Application.Products.Activities;
using CoolShop.Catalog.Application.Products.Workflows;

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

        builder.Services.AddDaprWorkflowClient();
        builder.Services.AddDaprWorkflow(options =>
        {
            options.RegisterWorkflow<ReduceQuantityWorkflow>();
            options.RegisterActivity<RetrieveInventoryActivity>();
            options.RegisterActivity<SetOutStockProductActivity>();
            options.RegisterActivity<UpdateInventoryActivity>();
        });
    }
}
