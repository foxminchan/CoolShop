using CoolShop.Cart.Extensions;
using CoolShop.Cart.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.UseOpenApi();

app.UseCloudEvents();

app.MapSubscribeHandler();

app.MapSubscribers();

app.MapGrpcService<BasketService>();

app.Run();
