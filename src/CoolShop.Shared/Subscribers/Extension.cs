namespace CoolShop.Shared.Subscribers;

public static class Extension
{
    public static void AddSubscribers(this IHostApplicationBuilder builder, Type type)
    {
        builder.Services.Scan(scan => scan
            .FromAssembliesOf(type)
            .AddClasses(classes => classes.AssignableTo<ISubscriber>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public static IApplicationBuilder MapSubscribers(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var subscribers = scope.ServiceProvider.GetRequiredService<IEnumerable<ISubscriber>>();

        IEndpointRouteBuilder builder = app;

        foreach (var endpoint in subscribers)
        {
            endpoint.MapSubscriber(builder);
        }

        return app;
    }
}
