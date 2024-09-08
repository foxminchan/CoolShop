namespace CoolShop.Catalog.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults()
            .AddInfrastructure()
            .AddApplication();

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.Converters.Add(new StringTrimmerJsonConverter());
        });

        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<UniqueConstraintExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.AddOpenApi();
        builder.AddVersioning();
        builder.AddEndpoints(typeof(global::Program));
        builder.AddSubscribers(typeof(global::Program));

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
                    options.Audience = ServiceName.AppId.Cart;
                    options.RequireHttpsMetadata = false;
                });

        builder.Services.AddAuthorization();
    }
}
