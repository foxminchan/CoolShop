namespace CoolShop.Notification.Extensions;

internal static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDaprClient();

        builder.AddSubscribers(typeof(Program));
    }
}
