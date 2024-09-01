using CoolShop.Cart.Extensions;
using CoolShop.Cart.Grpc;
using CoolShop.ServiceDefaults;
using CoolShop.Shared.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.UseOpenApi();

app.UseCloudEvents();

app.MapGrpcService<BasketService>();

app.Run();
