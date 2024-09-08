namespace CoolShop.Promotion.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
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
        builder.AddMongoDBClient(ServiceName.Database.Rating);

        builder.Services.AddScoped(provider =>
            provider.GetRequiredService<IMongoDatabase>().GetCollection<Coupon>(nameof(Coupon)));

        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });

        builder.Services.AddDaprClient();

        builder.Services.AddAuthentication()
            .AddKeycloakJwtBearer(ServiceName.Keycloak,
                nameof(CoolShop),
                options =>
                {
                    options.Audience = ServiceName.AppId.Promotion;
                    options.RequireHttpsMetadata = false;
                });

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
    }
}
