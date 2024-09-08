using CoolShop.Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpContextAccessor();
builder.Services.AddBff().AddRemoteApis();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddBffExtensions();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "__Host-bff";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddKeycloakOpenIdConnect(ServiceName.Keycloak, nameof(CoolShop), options =>
    {
        options.RequireHttpsMetadata = false;
        options.ClientId = ServiceName.AppId.Gateway;
        options.ResponseType = OpenIdConnectResponseType.Code;
    });

builder.AddRateLimiting();

var app = builder.Build();

app.UseRateLimiter();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseBff();

app.MapBffManagementEndpoints();

app.MapDefaultEndpoints();

app.Run();
