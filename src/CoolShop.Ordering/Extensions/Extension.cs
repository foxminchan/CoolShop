namespace CoolShop.Ordering.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.AddPersistence();

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.Converters.Add(new StringTrimmerJsonConverter());
        });

        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<global::Program>();
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(MetricsBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssemblyContaining<global::Program>(includeInternalTypes: true);

        builder.Services.AddSingleton<IActivityScope, ActivityScope>();
        builder.Services.AddSingleton<CommandHandlerMetrics>();
        builder.Services.AddSingleton<QueryHandlerMetrics>();

        builder.AddOpenApi();
        builder.AddVersioning();
        builder.AddEndpoints(typeof(global::Program));

        builder.Services.AddGrpc();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddDaprClient();
        builder.Services.AddDaprWorkflowClient();
        builder.Services.AddDaprWorkflow(options =>
        {
            options.RegisterWorkflow<CreateOrderWorkflow>();
            options.RegisterWorkflow<CancelOrderWorkflow>();
            options.RegisterActivity<NotifyActivity>();
            options.RegisterActivity<AddOrderActivity>();
            options.RegisterActivity<RefundOrderActivity>();
            options.RegisterActivity<RetrieveBasketActivity>();
            options.RegisterActivity<CancelOrderActivity>();
        });
    }
}
