var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseCloudEvents();

app.MapSubscribeHandler();

app.MapSubscribers();

app.Run();
